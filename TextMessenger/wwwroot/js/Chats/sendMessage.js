const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

hubConnection.on("ReceiveMessage", function (message) {
    console.log("m=" + message.type);
    switch (message.type) {
        case 0:
            let personalChatList = $('.personal-chat-list');
            UpdateChatList(personalChatList, message);
            break;
        case 1:
            let groupChatList = $('.group-chat-list');
            UpdateChatList(groupChatList, message);
            break;
    }
    let messageList = $('.message-list');
    AddMessageToMessageList(messageList, message);
    $('textarea[name="message_input"]').val('');
});

function sendMessage() {
    let chatId = parseInt($('#chatId').text());
    let message = $('textarea[name="message_input"]').val();
    if (message == "") return;
    hubConnection.invoke("SendMessage", {"chatId":chatId, "text":message}).catch(function (err) {
        return console.error(err.toString());
    });
}

hubConnection.start().catch(function (err) {
    return console.error(err.toString());
});

function UpdateChatList(chatList, message) {
    let chatListElement = $(chatList).find('li[name="' + message.chatId + '"]');
    let newChatListElement = $(chatList).find('li[name="empty_chat"]').clone(true);
    if (chatListElement.length) {
        $(newChatListElement).find('span[name="chatname"]').text($(chatListElement).find('span[name="chatname"]').text());
        $(chatListElement).remove();
    }
    else {
        if (message.Type == 0) {
            $(newChatListElement).find('span[name="chatname"]').text(message.nickname);
        }
        else {
            $(newChatListElement).find('span[name="chatname"]').text(message.chatName);
        }
    }
    $(newChatListElement).attr('name', message.chatId);
    $(newChatListElement).find('span[name="date"]').text(message.date);
    $(newChatListElement).find('span[name="message"]').text(message.text);
    $(chatList).find('li[name="empty_chat"]').after(newChatListElement);
}

function AddMessageToMessageList(messageList, message) {
    let newMessage = $(messageList).find('li[name="empty_message"]').clone(true);
    $(newMessage).attr('name', message.messageId);
    if (message.isSystem) {
        $(newMessage).addClass('system-message');
    }
    else {
        let userId = $('#userId').text();
        $(newMessage).find('span[name="nickname"]').text(message.nickname);
        $(newMessage).find('span[name="date"]').text(message.date);
        if (message.userId == userId) $(newMessage).addClass('personal-message');
        else $(newMessage).addClass('other-user-message');
    }
    $(newMessage).find('span[name="message"]').text(message.text);
    $(messageList).append(newMessage);
}