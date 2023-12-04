function loadMoreMessages() {
    let chatId = $("#chatId").text();
    let firstLoadedMessageId = $('.message-list').find('li:not([name="empty_message"]):first').attr("name");

    $.ajax({
        url: `/Chats/GetMoreMessages?chatId=${chatId}&firstLoadedMessageId=${firstLoadedMessageId}`,
        type: "POST",
        success: function (response) {
            if (response.statusCode == 200) {
                AddNewMessagesFromJsonList(response.data);
                if (!response.data.hasMoreMessages) {
                    $('.load-more-messages-container').css('visibility', 'hidden');
                }
            }
        },
        error: function (jqXHR, exception) {
            alert("Ошибка");   //Пока что заглушка
        }
    });
}

function AddNewMessagesFromJsonList(data) {
    let message_list = $('.message-list');
    let empty_message = $(message_list).find('li[name="empty_message"]');
    for (var message of data.messages)
    {
        let new_message = $(empty_message).clone(true);
        $(new_message).attr('name', message.messageId);
        if (message.isSystem)
        {
            $(new_message).addClass('system-message');
        }
        else
        {
            $(new_message).find('span[name="nickname"]').text(message.userNickname);
            $(new_message).find('span[name="date"]').text(message.date);
            if (message.messageCreatorId == data.userId) $(new_message).addClass('personal-message');
            else $(new_message).addClass('other-user-message');
        }
        $(new_message).find('span[name="message"]').text(message.text);
        $(empty_message).after(new_message);
    }
}