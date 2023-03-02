function deepExtend(out, ...arguments_) {
    if (!out) {
      return {};
    }
  
    for (const obj of arguments_) {
      if (!obj) {
        continue;
      }
  
      for (const [key, value] of Object.entries(obj)) {
        switch (Object.prototype.toString.call(value)) {
          case '[object Object]':
            out[key] = deepExtend(out[key], value);
            break;
          case '[object Array]':
            out[key] = deepExtend(new Array(value.length), value);
            break;
          default:
            out[key] = value;
        }
      }
    }
  
    return out;
  }
  

var stack = function (selector, options) {
    var tables = document.querySelectorAll(selector);
    var defaults = { id: 'stacktable small-only', hideOriginal: true, headIndex: 0 };
    var settings = deepExtend({}, defaults, options);

    console.log(settings)

    // checking the "headIndex" option presence... or defaults it to 0
    if (options && options.headIndex)
        headIndex = options.headIndex;
    else
        headIndex = 0;

    return tables.forEach((table) => {
        var table_css = table.className;
        var stacktable = $('<table class="' + table_css + ' ' + settings.id + '"><tbody></tbody></table>');
        if (typeof settings.myClass !== 'undefined') 
            stacktable.classList.add(settings.myClass);

        var markup = '';

        table.classList.add('stacktable large-only');
        $topRow = $table.find('tr').eq(0);

        // using rowIndex and cellIndex in order to reduce ambiguity
        $table.find('tr').each(function (rowIndex, value) {

            // declaring headMarkup and bodyMarkup, to be used for separately head and body of single records
            headMarkup = '';
            bodyMarkup = '';
            tr_class = $(this).prop('class');
            // for the first row, "headIndex" cell is the head of the table
            if (rowIndex === 0) {
                // the main heading goes into the markup variable
                markup += '<tr class=" ' + tr_class + ' "><th class="st-head-row st-head-row-main" colspan="2">' + $(this).find('th,td').eq(headIndex).html() + '</th></tr>';
            }
            else {
                // for the other rows, put the "headIndex" cell as the head for that row
                // then iterate through the key/values
                $(this).find('td,th').each(function (cellIndex, value) {
                    if (cellIndex === headIndex) {
                        headMarkup = '<tr class="' + tr_class + '"><th class="st-head-row" colspan="2">' + $(this).html() + '</th></tr>';
                    } else {
                        if ($(this).html() !== '') {
                            bodyMarkup += '<tr class="' + tr_class + '">';
                            if ($topRow.find('td,th').eq(cellIndex).html()) {
                                bodyMarkup += '<td class="st-key">' + $topRow.find('td,th').eq(cellIndex).html() + '</td>';
                            } else {
                                bodyMarkup += '<td class="st-key"></td>';
                            }
                            bodyMarkup += '<td class="st-val ' + $(this).prop('class') + '">' + $(this).html() + '</td>';
                            bodyMarkup += '</tr>';
                        }
                    }
                });

                markup += headMarkup + bodyMarkup;
            }
        });

        $stacktable.append($(markup));
        $table.before($stacktable);
        if (!settings.hideOriginal) $table.show();
    });
}