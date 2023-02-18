(function () {
  'use strict';

  function previewResource($http, umbRequestHelper) {

    var apiUrl = Umbraco.Sys.ServerVariables.TwentyFourDays.PreviewApi;

    var resource = {
      getPreview: getPreview
    };

    return resource;

    function getPreview(data, settings, pageId, culture) {
        var dataSet = {
            data : data,
            settings : settings
        };

        console.log(dataSet)

      return umbRequestHelper.resourcePromise(
        $http.post(apiUrl + '?pageId=' + pageId + '&culture=' + culture, dataSet),
        'Failed getting preview markup'
      );
    };
  }

    angular.module('umbraco.resources').factory('TwentyFourDays.Resources.PreviewResource', previewResource);

})();