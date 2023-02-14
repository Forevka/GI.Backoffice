angular.module('umbraco.services').config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(['$q', '$injector', 'notificationsService', function ($q, $injector, notificationsService) {
        return {
            'request': function (request) {
                if (request.url.indexOf("views/common/infiniteeditors/mediapicker/mediapicker.html") != -1) {
                    var apiUrl = Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath + "/backoffice/UmbraCare/DedicatedMediaFolder/MediaPickerView";

                    request.url = appendCacheBuster(apiUrl);
                }

                return request || $q.when(request);
            }
        };
    }]);

    function appendCacheBuster(url) {
        var paramPrefix = url.indexOf('?') > 0 ? '&' : '?';

        if (!Umbraco.Sys.ServerVariables.application)
            return url + paramPrefix + "t=" + Date.now();

        var cacheBusterHash = Umbraco.Sys.ServerVariables.application.cacheBuster;

        return url + paramPrefix + "cbh=" + cacheBusterHash;
    }
}]);