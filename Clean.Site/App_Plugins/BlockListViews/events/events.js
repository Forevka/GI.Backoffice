app.run(function (eventsService) {

    // Subscribe to the event
    var unsubscribe2 = eventsService.on("app.ready", function(event, args) {
        var t = setInterval(() => {
            let item = document.querySelector("#sub-view-0 > div > umb-element-editor-content > div > ng-form > div > div > umb-property:nth-child(1) > div > ng-form > div > div.umb-el-wrap > div.control-header > small");
            
            let items = document.querySelectorAll('#sub-view-0 > div > umb-element-editor-content > div > ng-form > div > div > umb-property > div > ng-form > div > div.umb-el-wrap > div.controls > div > div > div > umb-block-list-property-editor > div > div > div.ng-pristine.ng-untouched.ng-valid.ui-sortable.ng-not-empty > div > umb-block-list-row > ng-form > div > umb-block-list-block > div > div > div > umb-element-editor-content > div > ng-form > div > div > umb-property > div > ng-form > div > div.umb-el-wrap > div.control-header > small:not([data-force-loaded])')
            
            items.forEach(element => {
                console.log('force image load')
                element.setAttribute('data-force-loaded', 'true')
                element.click()
            })

            if (item != null && (item.getAttribute('data-force-loaded') == null || item.getAttribute('data-force-loaded') == 'false'))
            {
                console.log('force image load')
                item.setAttribute('data-force-loaded', 'true')
                item.click()
            }
        }, 500);
    });
    

});