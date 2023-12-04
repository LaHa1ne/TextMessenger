$(document).ready(function () {
    calculateHeight();

    $(window).resize(function () {
        calculateHeight();
    });
});

function calculateHeight() {
    var headerHeight = $('header').outerHeight();
    var footerHeight = $('footer').outerHeight();
    var windowHeight = $(window).height();
    var mainChatsContainerHeight = windowHeight - headerHeight - footerHeight;
    $('.main-chats-container').height(mainChatsContainerHeight);

    // 1. Столбец со списком чатов
    var chatListColHeight = $('.chat-list-col').outerHeight();
    var createChatSectionHeight = $('.create-chat-section').outerHeight();

    // 1.1. Секции личных и групповых чатов
    var personalChatSectionHeight = (chatListColHeight - createChatSectionHeight) / 2;
    var groupChatSectionHeight = (chatListColHeight - createChatSectionHeight) / 2;
    $('.personal-chat-section').height(personalChatSectionHeight);
    $('.group-chat-section').height(groupChatSectionHeight);

    // 1.1.1. Контейнеры со списками чатов
    var personalChatSectionCaptionContainer = $('.personal-chat-section-caption-container').outerHeight();
    $('.personal-chat-list-container').height(personalChatSectionHeight - personalChatSectionCaptionContainer);
    var groupChatSectionCaptionContainer = $('.group-chat-section-caption-container').outerHeight();
    $('.group-chat-list-container').height(groupChatSectionHeight - groupChatSectionCaptionContainer);

    // 2. Столбец с списком сообщений
    var chatColHeight = $('.chat-col').outerHeight();
    var inputMessageSectionHeight = $('.input-message-section').outerHeight();

    // 2.1. Секция cо списком сообщений
    var messageListSectionHeight = chatColHeight - inputMessageSectionHeight;
    $('.message-list-section').height(messageListSectionHeight);

    // 2.1.1. Контейнер со списком сообщений
    var messageListSectionCaptionContainerHeight = $('.message-list-section-caption-container').outerHeight();
    var loadMoreMessagesContainerHeight = $('.load-more-messages-container').outerHeight();
    $('.message-list-container').height(messageListSectionHeight - messageListSectionCaptionContainerHeight - loadMoreMessagesContainerHeight);

    // 3. Столбец с участниками чата
    var chatMembersColHeight = $('.chat-members-col').outerHeight();
    var chatMembersSectionHeight = chatMembersColHeight;
    $('.chat-members-section').height(chatMembersSectionHeight);

    var chatMembersSectionCaptionContainerHeight = $('.chat-members-section-caption-container').outerHeight();
    $('.chat-members-list-container').height(chatMembersSectionHeight - chatMembersSectionCaptionContainerHeight);

    




}
