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
                AddMessage(userName, msg, date, "/Images/dummy.jpg");
                chatHub.server.sendPrivateMessage("dennis", msg, date);
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
            AddMessage(messages[i].UserName, messages[i].Message, messages[i].Time, messages[i].UserImage);

        }
    }

    // On New User Connected
    chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) {
        AddUser(chatHub, id, name, UserImage, loginDate);
    }

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {

        $('#Div' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();


        var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);

    }

    chatHub.client.messageReceived = function (userName, message, time, userimg) {

        AddMessage(userName, message, time, userimg);
        
    }


    //chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userimg, CurrentDateTime) {

    //    var CurrUser = $('#userName');
    //    var Side = 'left';
    //    var TimeSide = 'right';

    //    if (CurrUser == fromUserName) {
    //        Side = 'right';
    //        TimeSide = 'left';

    //    }
    //    else {
    //        var Notification = 'New Message From ' + fromUserName;
    //        IntervalVal = setInterval("ShowTitleAlert('SignalR Chat App', '" + Notification + "')", 800);

    //        var msgcount = $('#' + ctrId).find('#MsgCountP').html();
    //        msgcount++;
    //        $('#' + ctrId).find('#MsgCountP').html(msgcount);
    //        $('#' + ctrId).find('#MsgCountP').attr("title", msgcount + ' New Messages');
    //    }

    //    var divChatP = '<div class="direct-chat-msg ' + Side + '">' +
    //        '<div class="direct-chat-info clearfix">' +
    //        '<span class="direct-chat-name pull-' + Side + '">' + fromUserName + '</span>' +
    //        '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + CurrentDateTime + '</span>' +
    //        '</div>' +

    //        ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
    //        ' <div class="direct-chat-text" >' + message + '</div> </div>';

    //    $('#' + ctrId).find('#divMessage').append(divChatP);

    //    var htt = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
    //    $('#' + ctrId).find('#divMessage').slimScroll({
    //        height: htt
    //    });
    //}

}

function GetCurrentDateTime(now) {

    var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

    return localdate;
}

function AddUser(chatHub, id, name, UserImage, date) {

    var userId = $('#hdId').val();

    var code, Clist;
    if (userId == id) {

        code = $('<div class="box-comment">' +
            '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
            ' <div class="comment-text">' +
            '<span class="username">' + name + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');


        Clist = $(
            '<li style="background:#494949;">' +
            '<a href="#">' +
            '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

            ' <div class="contacts-list-info">' +
            ' <span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
            ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');

    }
    else {

        code = $('<div class="box-comment" id="Div' + id + '">' +
            '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
            ' <div class="comment-text">' +
            '<span class="username">' + '<a id="' + id + '" class="user" >' + name + '<a>' + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');


        Clist = $(
            '<li>' +
            '<a href="#">' +
            '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

            ' <div class="contacts-list-info">' +
            '<span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
            ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');


        var UserLink = $('<a id="' + id + '" class="user" >' + name + '<a>');
        $(code).click(function () {

            var id = $(UserLink).attr('id');

            if (userId != id) {
                var ctrId = 'private_' + id;
                OpenPrivateChatBox(chatHub, id, ctrId, name);

            }

        });

        var link = $('<span class="contacts-list-name" id="' + id + '">');
        $(Clist).click(function () {

            var id = $(link).attr('id');

            if (userId != id) {
                var ctrId = 'private_' + id;
                OpenPrivateChatBox(chatHub, id, ctrId, name);

            }

        });

    }

    $("#divusers").append(code);

    $("#ContactList").append(Clist);

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

// Creation and Opening Private Chat Div
function OpenPrivateChatBox(chatHub, userId, ctrId, userName) {

    var PWClass = $('#PWCount').val();

    if ($('#PWCount').val() == 'info')
        PWClass = 'danger';
    else if ($('#PWCount').val() == 'danger')
        PWClass = 'warning';
    else
        PWClass = 'info';

    $('#PWCount').val(PWClass);
    var div1 = ' <div class="col-md-4"> <div  id="' + ctrId + '" class="box box-solid box-' + PWClass + ' direct-chat direct-chat-' + PWClass + '">' +
        '<div class="box-header with-border">' +
        ' <h3 class="box-title">' + userName + '</h3>' +

        ' <div class="box-tools pull-right">' +
        ' <span data-toggle="tooltip" id="MsgCountP" title="0 New Messages" class="badge bg-' + PWClass + '">0</span>' +
        ' <button type="button" class="btn btn-box-tool" data-widget="collapse">' +
        '    <i class="fa fa-minus"></i>' +
        '  </button>' +
        '  <button id="imgDelete" type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button></div></div>' +

        ' <div class="box-body">' +
        ' <div id="divMessage" class="direct-chat-messages">' +

        ' </div>' +

        '  </div>' +
        '  <div class="box-footer">' +


        '    <input type="text" id="txtPrivateMessage" name="message" placeholder="Type Message ..." class="form-control"  />' +

        '  <div class="input-group">' +
        '    <input type="text" name="message" placeholder="Type Message ..." class="form-control" style="visibility:hidden;" />' +
        '   <span class="input-group-btn">' +
        '          <input type="button" id="btnSendMessage" class="btn btn-' + PWClass + ' btn-flat" value="send" />' +
        '   </span>' +
        '  </div>' +

        ' </div>' +
        ' </div></div>';



    var $div = $(div1);

    // Closing Private Chat Box
    $div.find('#imgDelete').click(function () {
        $('#' + ctrId).remove();
    });

    // Send Button event in Private Chat
    $div.find("#btnSendMessage").click(function () {

        $textBox = $div.find("#txtPrivateMessage");

        var msg = $textBox.val();
        if (msg.length > 0) {
            chatHub.server.sendPrivateMessage(userId, msg);
            $textBox.val('');
        }
    });

    // Text Box event on Enter Button
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });

    // Append private chat div inside the main div
    $('#PriChatDiv').append($div);
}