angular.module("mainModule")
    .controller("AlarmController", [
        "$scope",
        "$routeParams",
        "$rootScope",
        "Hub",
        "alarmsApi",
        function ($scope, $routeParams, $rootScope, Hub, alarmsApi) {

            //$scope.newMessage = {};

            var signalrPath = 'http://localhost:60136/api/alarmsAPI/signalr';
            var hub = new Hub('alarmHub', {

                rootPath: signalrPath,
                listeners: {
                    'recieveMessage': function (message) {
                        $rootScope.message = message;
                        angular.forEach($scope.data.alarms, function (alarm) {
                            alarm.messages.push(message);
                        });
                        $rootScope.$apply();
                    }
                },

                errorHandler: function (error) {
                    console.error(error);
                },

                stateChanged: function (state) {
                    switch (state.newState) {
                        case $.signalR.connectionState.connecting:
                            console.log("signalR.connectionState.connecting" + state.newState);
                            break;
                        case $.signalR.connectionState.connected:
                            console.log("signalR.connectionState.connected" + state.newState);
                            break;
                        case $.signalR.connectionState.reconnecting:
                            console.log("signalR.connectionState.reconnecting" + state.newState);
                            break;
                        case $.signalR.connectionState.disconnected:
                            console.log("signalR.connectionState.disconnected" + state.newState);
                            break;
                    }
                }

            });
        }
    ]);