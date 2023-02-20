app.run(function (eventsService) {

    // Subscribe to the event
    var unsubscribe2 = eventsService.on("app.ready", function(event, args) {
        var t = setInterval(() => {
            let item = document.querySelector("#sub-view-0 > div > umb-element-editor-content > div > ng-form > div > div > umb-property:nth-child(1) > div > ng-form > div > div.umb-el-wrap > div.control-header > small");
            
            if (item != null && (item.getAttribute('data-force-loaded') == null || item.getAttribute('data-force-loaded') == 'false'))
            {
                console.log('force image load')
                item.setAttribute('data-force-loaded', 'true')
                item.click()
            }
        }, 100);
    });
    

});