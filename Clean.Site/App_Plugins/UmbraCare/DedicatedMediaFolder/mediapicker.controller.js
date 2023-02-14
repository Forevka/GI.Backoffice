'use strict';
angular.module('umbraco').controller('UmbraCare.DedicatedMediaFolder.MediaPickerController', function ($scope, $controller, $routeParams, $http, editorState, umbRequestHelper, overlayService, notificationsService, localStorageService) {
    var extendedVm = this;

    extendedVm.goToDedicatedMediaFolder = goToDedicatedMediaFolder;

    var dialogOptions = $scope.model;

    $scope.startNodeId = dialogOptions.startNodeId ? dialogOptions.startNodeId : -1;

    var isStartNodeSetByMediaPickerPropertyEditor = $scope.startNodeId && $scope.startNodeId != -1
        && dialogOptions.startNodeIsVirtual === undefined;

    $scope.enableDedicatedMediaFolder = isEditorStateUseful() || $routeParams.section == "content";

    $scope.dedicatedMediaFolder = null;

    if ($scope.enableDedicatedMediaFolder)
        goToDedicatedMediaFolder(false);
    else
        initBaseController();

    function initBaseController() {
        var baseController = $controller('Umbraco.Editors.MediaPickerController as vm', { $scope: $scope });

        angular.extend(extendedVm, baseController);
    }

    function isInitializedBaseController() {
        return extendedVm.gotoFolder;
    }

    function goToDedicatedMediaFolder(createIfNotExists) {
        if (!isDedicatedMediaFolderAvailable(createIfNotExists)) {
            if (!isInitializedBaseController())
                initBaseController();

            return;
        }

        if ($scope.dedicatedMediaFolder) {
            goToFolder($scope.dedicatedMediaFolder);

            return;
        }

        getDedicatedMediaFolder(createIfNotExists).then(function (dedicatedMediaFolder) {
            $scope.model.creatingFolder = false;

            if (dedicatedMediaFolder && dedicatedMediaFolder.id && dedicatedMediaFolder.id != -1) {
                $scope.dedicatedMediaFolder = dedicatedMediaFolder;

                if (isInitializedBaseController())
                    goToFolder($scope.dedicatedMediaFolder);
                else {
                    localStorageService.set('umbLastOpenedMediaNodeId', $scope.dedicatedMediaFolder.id);

                    initBaseController();
                }
            }
            else {
                notificationsService.warning("Dedicated media folder doesn't exist yet. Just click Create dedicated media folder.");

                if (!isInitializedBaseController())
                    initBaseController();
            }
        }, function (error) {
            $scope.model.creatingFolder = false;

            if (!isInitializedBaseController())
                initBaseController();

            if (error.status && error.status >= 500)
                overlayService.ysod(error);
        });
    }

    function getDedicatedMediaFolder(createIfNotExists) {
        $scope.model.creatingFolder = true;

        var apiUrl = Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath + "/backoffice/UmbraCare/DedicatedMediaFolder/Get";

        return umbRequestHelper.resourcePromise($http.get(apiUrl, {
            params: {
                contentId: isEditorStateUseful() ? editorState.current.id : $routeParams.id,
                createIfNotExists: createIfNotExists
            }
        }), "Failed to retrieve the dedicated media folder.");
    }

    function isDedicatedMediaFolderAvailable(createIfNotExists) {
        var isUseful = isEditorStateUseful();

        if (isUseful && editorState.current.id == 0 || $routeParams.create) {
            notificationsService.warning("Dedicated media folder isn't available yet. Please save your content draft first.");

            return false;
        }

        if (isUseful && editorState.current.trashed) {
            notificationsService.warning("Dedicated media folder isn't available for the trashed content.");

            return false;
        }

        if (isStartNodeSetByMediaPickerPropertyEditor) {
            if (createIfNotExists)
                notificationsService.warning("Dedicated media folder is not available if the Start node is set in the Media Picker property editor.");

            return false;
        }

        return true;
    }

    function goToFolder(folder) {
        (extendedVm.gotoFolder || $scope.gotoFolder)(folder);
    }

    function isEditorStateUseful() {
        return editorState.current && editorState.current.hasOwnProperty("documentType");
    }
});