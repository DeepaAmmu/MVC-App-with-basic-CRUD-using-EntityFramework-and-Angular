(function () {

    var app = angular.module("dropdownsapp", []);

    app.controller('dropdowncontroller', function ($scope, $http) {

        $scope.GetCities = function () {
            if ($scope.StateId) {
                debugger;
                $http({
                    method: 'POST',
                    url: '/Home/GetCityList/',
                    data: JSON.stringify({ StateId:$scope.StateId })
                }).then(function (response) {
                    debugger;
                    $scope.message = "success";

                    $scope.cities = response.data;
                }, function (error) {
                    debugger;
                    $scope.message = "failure";
                    $scope.cities = null;
                });

                debugger;
            }
            else {
                $scope.cities = null;
            }

        }

    });


}());