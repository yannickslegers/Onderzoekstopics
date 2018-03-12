var IntervalVal;

// Declare a proxy to reference the hub.

var chatHub = $.connection.chatHub;
registerClientMethods(chatHub);
// Start Hub
$.connection.hub.start().done(function () {

    registerEvents(chatHub)

});

function registerEvents(chatHub) {

    var name = $('#userName').text();

    if (name.length > 0) {
        chatHub.server.connect(name);

    }

    // Send Button Click Event
    $('#sendBtn').click(function () {

        var msg = $("#messageText").val();

        if (msg.length > 0) {

            var userName = $('#userName').text();

            var date = GetCurrentDateTime(new Date());
            if (userName == "admin") {
                var toUser = $('.selectedUser').text();
                AddMessage(userName, msg, date, "/Images/dummy.jpg");
                chatHub.server.sendPrivateMessage(toUser, msg, date);
            }
            else {
                AddMessage(userName, msg, date, "/Images/dummy.jpg");
                chatHub.server.sendPrivateMessage("admin", msg, date);
            }
            $("#messageText").val('');
        }
    });

    // Send Message on Enter Button
    $("#messageText").keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $('#sendBtn').click();
        }
    });

    //Clear Cache Button Click Event
    $('#clearBtn').click(function () {
        var msg = $('.messages').html();
        if (msg.length > 0) {
            chatHub.server.clearMessageCache();
            $('.messages').html('');
        }
        
    })
}

function registerClientMethods(chatHub) {


    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages, times) {

        $('#userName').text(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].UserImage, allUsers[i].LoginTime);
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {
            AddMessage(messages[i].UserName, messages[i].Text, messages[i].Time, messages[i].UserImage);

        }
    }

    // On New User Connected
    chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) {
        AddUser(chatHub, id, name, UserImage, loginDate);
    }

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {

        $('.' + id).remove();

        var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

        $(disc).hide();
        $('.users').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);

    }

    chatHub.client.messageReceived = function (userName, message, time, userimg) {

        AddMessage(userName, message, time, userimg);
        
    }

}

function GetCurrentDateTime(now) {

    var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

    return localdate;
}

function AddUser(chatHub, id, name, UserImage, date) {

    var userName = $('#userName').text();

    var code;
    
    if(userName != name) {

        code = $('<div class="box-user '+id+'" id="'+name+'">' +
            '<img class="img-circle img-user" src="' + UserImage + '" alt="User Image" />' +
            ' <div class="user-text">' +
            '<p class="user-username">' + '<b id="' + id +'" class="un">' + name + '</b></p>' + '<p class="text-muted pull-right user-date"> Logged in: ' + date + '</p>  </div></div>');


        //TODO: selected methode fixen zodat niet alles tegelijk geselecteerd wordt
        $(code).click(function () {
            var UserLink = $(this).find('.un').text();
            
            $('.selecteduser').empty();
            $('.selectedUser').text(UserLink);
            $('.selected').removeClass('selected');
            $('#'+UserLink).addClass('selected');
            

        });
    }

    $('.users').append(code);
    

}

function AddMessage(userName, message, time, userimg) {

    var CurrUser = $('#userName').text();
    var Side = 'left';
    var TimeSide = 'right';

    if (CurrUser == userName) {
        Side = 'right';
        TimeSide = 'left';

    }

    var divChat = '<div class="chat-msg ' + Side + '">' +
        '<div class="chat-info clearfix">' +
        '<span class="chat-name pull-' + Side + '">' + userName + '</span>' +
        '<span class="chat-timestamp pull-' + TimeSide + '"">' + time + '</span>' +
        '</div>' +

        ' <img class="chat-img" src="' + userimg + '" alt="User Image">' +
        ' <div class="chat-text" >' + message + '</div> </div>';

    $('.messages').append(divChat);

    var height = $('.messages')[0].scrollHeight;
    $('.messages').scrollTop(height);

}

