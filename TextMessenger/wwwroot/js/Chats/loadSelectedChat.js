function loadSelectedChat(chatId) {
    $.ajax({
        url: `/Chats/GetSelectedChat?chatId=${chatId}`,
        type: "POST",
        beforeSend: function() {
            clearChatAndChatMembersSection()
        },
        success: function (response) {
            if (response.statusCode == 200) {
                AddMessagesFromJsonList(response.data);
                AddChatMembersFromJsonList(response.data);
                $('.message-list-section-caption-container').text(response.data.name);

                $("#chatId").text(response.data.chatId);
                $("#userId").text(response.data.userId);

                $('.chat-col').show();
                $('.chat-members-col').show();
                $('.load-more-messages-container').css('visibility', 'visible');
                calculateHeight();
            }
        },
        error: function (jqXHR, exception) {
            alert("Ошибка");   //Пока что заглушка
        }
    });
}

function clearChatAndChatMembersSection() {
    $("#chatId").text("");
    $("#userId").text("");

    $('.chat-col').hide();
    $('.chat-members-col').hide();

    $('.message-list').find('li:not([name="empty_message"])').remove();
    $('.chat-members-list').find('li:not([name="empty_chatMember"])').remove();

    $('textarea[name="message_input"]').val('');
}

function AddMessagesFromJsonList(data) {
    let message_list = $('.message-list');
    let empty_message = $(message_list).find('li[name="empty_message"]');
    for (var message of data.chatMessages) {
        let new_message = $(empty_message).clone(true);
        $(new_message).attr('name', message.messageId);
        if (message.isSystem) {
            $(new_message).addClass('system-message');
        }
        else {
            $(new_message).find('span[name="nickname"]').text(message.userNickname);
            $(new_message).find('span[name="date"]').text(message.date);
            if (message.messageCreatorId == data.userId) $(new_message).addClass('personal-message');
            else $(new_message).addClass('other-user-message');
        }
        $(new_message).find('span[name="message"]').text(message.text);
        $(empty_message).after(new_message);
    }
}

function AddChatMembersFromJsonList(data) {
    let chatMembers_list = $('.chat-members-list');
    let empty_chatMember = $(chatMembers_list).find('li[name="empty_chatMember"]');
    for (var chatMember of data.chatMembers) {
        let new_chatMember = $(empty_chatMember).clone(true);
        $(new_chatMember).attr('name', chatMember.userId);
        $(new_chatMember).find('span[name="nickname"]').text(chatMember.nickname);
        $(chatMembers_list).append(new_chatMember);
    }
}