$(document).ready(function () {
    var chatId = $('#chatId').text();

    if (chatId != "") {
        $('.chat-col').show();
        $('.chat-members-col').show();
    }
    else {
        $('.chat-col').hide();
        $('.chat-members-col').hide();
    }

    $('.load-more-messages-button').on('click', function () {
        loadMoreMessages();
    });

    $('.personal-chat-list').on('click', 'li', function () {
        let chatId = $(this).attr('name');
        loadSelectedChat(chatId);
    });

    $('.group-chat-list').on('click', 'li', function () {
        let chatId = $(this).attr('name');
        loadSelectedChat(chatId);
    });

    $('.send-message-button').on('click', function () {
        sendMessage();
    });

    $('.open-create-chat-model-form').on('click', function () {
        openCreateChatModalForm();
    });

    $('#CreateChatModalForm .btn-primary').on('click', function () {
        let createChatForm = $('#createChatForm');
        CreateChat(createChatForm);
    });
});