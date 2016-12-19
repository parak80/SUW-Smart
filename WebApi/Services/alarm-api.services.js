angular.module("mainModule")
    .service("alarmApi", [
        "$http",
        "$q",
            function ($http, $q) {
            	var api = "http://localhost:60136/api/alarmsAPI/";
                var alarms = api + "/alarms";
                //var messages = api + "/messages";

                this.getAllAlarm = function () {
                    var deferred = $q.defer();
                    $http.get(alarms)
                        .then(function (response) {
                            this.alarms = response.data;
                            deferred.resolve(response.data);
                        }, function (response) {
                            deferred.resolve([]);
                        });

                    return deferred.promise;
                }

                this.addAlarm = function (newAlarm) {
                    var deferred = $q.defer();
                    $http.post(alarms, newAlarm)
                        .then(function (response) {
                            deferred.resolve(response.data);
                        }, function (response) {
                            deferred.resolve([]);
                        });

                    return deferred.promise;
                }

                this.getAlarm = function () {
                    var deferred = $q.defer();
                    $http.get(alarms)
                        .then(function (response) {
                            this.alarms = response.data;
                            deferred.resolve(response);
                        }, function () {
                            deferred.resolve([]);
                        });

                    return deferred.promise;
                }

                this.deleteAlarm = function (id) {
                    var deferred = $q.defer();
                    $http.delete(alarms + "/" + id)
                        .then(function (response) {
                            deferred.resolve();
                        }, function (response) {
                            deferred.resolve();
                        });

                    return deferred.promise;
                }
                this.postAlarm = function (newAlarm) {
                    var deferred = $q.defer();
                    $http.post(alarms , newAlarm)
                        .then(function (response) {
                            this.alarms = response;
                            deferred.resolve(response.data);
                        }, function (response) {
                            deferred.resolve();
                        });

                    return deferred.promise;
                }
            }
    ]);
