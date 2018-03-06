// Declare a proxy to reference the hub.

var chatHub = $.connection.chatHub;

// Start Hub
$.connection.hub.start().done(function () {

    // Send Button Click Event
    $('#sendBtn').click(function () {

        var msg = $("#messageText").val();

        if (msg.length > 0) {

            var userName = $("#userName").val();

            var date = GetCurrentDateTime(new Date());

            chatHub.server.sendMessageToAll(userName, msg, date);
            $("#messageText").val('').focus();
        }
    });

    // Send Message on Enter Button
    $("#messageText").keypress(function (e) {
        if (e.which === 13) {
            $('#sendBtn').click();
        }
    });
})

chatHub.client.messageReceived = function (username, message, userImg, time) {
    addMessage(username, message, userImg, time)
}

function addMessage(userName, message, time, userImg) {
    var currUser = $("#userName").val()

    if (currUser === userName) {
        $(".messages").append("<li class=\"right\"><div class=\"msgText\">" + message + "</div><img src=\"" + userImg + "\" class=\"userImage img-circle\"><div class=\"userName\">" + userName + "</div><div class=\"msgTime\">" + time + "</div></li>")
    }
    else {
        $(".messages").append("<li class=\"left\"><div class=\"msgText\">" + message + "</div><img src=\"" + userImg + "\" class=\"userImage img-circle\"><div class=\"userName\">" + userName + "</div><div class=\"msgTime\">" + time + "</div></li>")
    }

    //autoscroll to last message
    //$("#chatMessage").scrollTop($("#chatMessage")[0].scrollHeight)
}

function GetCurrentDateTime(now) {

    var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

    return localdate;
}

