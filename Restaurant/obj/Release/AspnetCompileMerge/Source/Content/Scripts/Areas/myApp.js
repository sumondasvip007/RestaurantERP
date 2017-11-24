
//(function () {
//    angular.module('myApp',[]);

//    //$(function () {
//    //    $.connection.hub.logging = true;
//    //    $.connection.hub.start();
//    //});
//    $.connection.hub.start(function () {
//        //chat.server.getAllOnlineStatus();
//    });


//    $.connection.hub.error(function (err) {
//        console.log('An error occurred: ' + err);
//    });

//    angular.module('myApp')
//       .value('notification', $.connection.notification)
//       .value('toastr', toastr);

//})();

//(function () {
//    angular.module('myApp');

//    $.connection.hub.start(function () {
//        var BrokerName = $('#BrokerName').val();
//        var UserName = $('#UserName').val();
//        $.connection.notification.server.getNotification(BrokerName, UserName);
//        $.connection.hub.logging = true;
//    });


//    $.connection.hub.error(function (err) {
//        console.log('An error occurred: ' + err);
//    });

//    angular.module('myApp')
//       .value('notification', $.connection.notification);

//})();
angular.module('myApp', []);