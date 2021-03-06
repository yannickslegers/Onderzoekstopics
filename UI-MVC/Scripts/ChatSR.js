﻿var IntervalVal;
var selectedUser = "broadcast";

// Proxy referentie naar hub declareren.
var chatHub = $.connection.chatHub;

registerClientMethods(chatHub);

// Start Hub
$.connection.hub.start().done(function () {

    registerEvents(chatHub);

});

function registerEvents(chatHub) {

    var name = $("#userName").text();

    if (name.length > 0) {
        chatHub.server.connect(name);
    }

    // Send Button Click Event
    $("#sendBtn").click(function () {

        var msg = $("#messageText").val();

        if (msg.length > 0) {

            var userName = $("#userName").text();

            var date = GetCurrentDateTime(new Date());
            if (userName === "admin") {
                var toUser = selectedUser;
                if (toUser === "broadcast") {
                    chatHub.server.sendMessageToAll("UPDATE: "+ msg, date);
                }
                else {
                    AddMessage(userName, msg, date, "/Images/dummy.jpg");
                    chatHub.server.sendPrivateMessage(toUser, msg, date);
                }
            }
            else {
                AddMessage(userName, msg, date, "/Images/dummy.jpg");
                chatHub.server.sendPrivateMessage("admin", msg, date);
            }
            $("#messageText").val("");
        }
    });

    // Send Message on Enter Button
    $("#messageText").keypress(function (e) {
        if (e.which === 13) {
            e.preventDefault();
            $("#sendBtn").click();
        }
    });

    // Broadcast Button click event
    $('#broadcast').click(function() {
        $('.selected').removeClass("selected");
        selectedUser = "broadcast";

        //Clear messages from screen
        var msg = $(".messages").html();
        if (msg.length > 0) {
            $(".messages").html("");
        }

        //Load messages from admin cache (broadcasts)
        chatHub.server.getMessageCache("admin");

    });
}

function registerClientMethods(chatHub) {


    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

        $("#userName").text(userName);

        // Add All Users
        for (var i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName, allUsers[i].UserImage, allUsers[i].LoginTime);
        }

        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {
            AddMessage(messages[i].UserName, messages[i].Text, messages[i].Time, messages[i].UserImage);

        }
    }

    // On New User Connected
    chatHub.client.onNewUserConnected = function (id, name, userImage, loginDate) {
        AddUser(chatHub, id, name, userImage, loginDate);
    }

    // On User Disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {

        
        $(`.${id}`).remove();

        var disc = $(`<div class="disconnect">"${userName}" logged off.</div>`);

        $(disc).hide();
        $(".users").prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);

    }
    // On message recieved
    chatHub.client.messageReceived = function (userName, message, time, userimg) {

        AddMessage(userName, message, time, userimg);
            
    }
    // load user specific message cache
    chatHub.client.reloadMessages = function (messages) {
        for (var i = 0; i < messages.length; i++) {
            AddMessage(messages[i].UserName, messages[i].Text, messages[i].Time, messages[i].UserImage);

        }
    }

}

function GetCurrentDateTime(now) {

    var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

    return localdate;
}

// Adds online user to current online users table
function AddUser(chatHub, id, name, userImage, date) {
    var userName = $("#userName").text();
    var code;
    
    if(userName !== name) {

        code = $(`<div class="box-user ${id}" id="${name}"><img class="img-circle img-user" src="${userImage
            }" alt="User Image" /> <div class="user-text"><p class="user-username"><b id="${id}" class="un">${name
            }</b></p><p class="text-muted pull-right user-date"> Logged in: ${date}</p>  </div></div>`);

        $(code).click(function () {
            var userLink = $(this).find(".un").text();
            
            selectedUser = userLink;
            $(".selected").removeClass("selected");
            $(`#${userLink}`).addClass("selected");

            //Clear messages from screen
            const msg = $('.messages').html();
            if (msg.length > 0) {
                $(".messages").html("");
            }
            //Load messages from selected user on screen
            chatHub.server.getMessageCache(selectedUser);
        });
    }
    $(".users").append(code);
}

// Adds message to message partial view
function AddMessage(userName, message, time, userimg) {

    var currUser = $("#userName").text();
    var side = "left";
    var timeSide = "right";

    if (currUser === userName) {
        side = "right";
        timeSide = "left";
    }
    var divChat = `<div class="chat-msg ${side}"><div class="chat-info clearfix"><span class="chat-name pull-${side}">${userName
        }</span><span class="chat-timestamp pull-${timeSide}"">${time}</span></div> <img class="chat-img" src="${userimg
        }" alt="User Image"> <div class="chat-text" >${message}</div> </div>`;
    $(".messages").append(divChat);
    var height = $(".messages")[0].scrollHeight;
    $(".messages").scrollTop(height);
}

