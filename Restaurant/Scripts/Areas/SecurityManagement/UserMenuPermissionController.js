angular.module('myApp').controller('UserPermissionController', function ($scope, $window, $http) {

  

    $scope.LoadInitial = function () {
        $http.get('/Account/GetAllUserForCurrentRestaurent')
            .success(function (data) {
                if (data.success) {
                    $scope.userList = data.data;
                } else {
                    toastr.error(data.errorMessage);
                }
            });

        //this.awesomeCallback = function (node, tree) {
        //    // Do something with node or tree
        //};

        //this.otherAwesomeCallback = function (node, isSelected, tree) {
        //    // Do soemthing with node or tree based on isSelected
        //}

    }
    $scope.LoadActionListForUser = function (userId) {
        $http.get('/Account/UserMenuActionForPermission?userId=' + userId)
            .success(function (data) {

                //$scope.rowCollection = data.data;
                //$scope.displayedCollection = [].concat($scope.rowCollection);
                $scope.bag = data.data;
            });
    };


    $scope.LoadReportListForUser = function (userId) {
        $http.get('/Account/UserReportActionForPermission?userId=' + userId)
            .success(function (data) {

                //$scope.rowCollection = data.data;
                //$scope.displayedCollection = [].concat($scope.rowCollection);
                $scope.bag = data.data;
            });
    };

    $scope.SaveUserPermission = function() {
        if ($scope.UserPermissionForm.$valid) {
        $scope.selectedMenu = [];
        for (var i = 0; i < $scope.bag.length; i++) {
            for (var j = 0; j < $scope.bag[i].children.length; j++) {
                if ($scope.bag[i].children[j].selected === true) {
                    $scope.selectedMenu.push($scope.bag[i].children[j].id);
                }
            }
        }
        $http({
            method: 'POST',
            url: '/Account/SaveUserPermission',
            data: { id: $scope.selectedMenu, userId: $scope.User.user_id }
        }).success(function(data) {
            if (data.success) {
                toastr.success("Submitted Successfully");

            } else {
                toastr.error(data.errorMessage);
            }
        });
    }
    else
    {
            toastr.error("Please select a  username");
    }
}

    //$scope.SaveUserReportPermission = function () {
    //    $scope.selectedMenu = [];
    //    for (var i = 0; i < $scope.bag.length; i++) {
    //        for (var j = 0; j < $scope.bag[i].children.length; j++) {
    //            if ($scope.bag[i].children[j].selected === true) {
    //                $scope.selectedMenu.push($scope.bag[i].children[j].id);
    //            }
    //        }
    //    }
    //    $http({
    //        method: 'POST',
    //        url: '/Account/SaveUserReportPermission',
    //        data: { id: $scope.selectedMenu, userId: $scope.User.user_id }
    //    }).success(function (data) {
    //        if (data.success) {
    //            toastr.success("Submitted Successfully");

    //        }
    //        else {
    //            toastr.error(data.errorMessage);
    //        }
    //    });

    //}
});
