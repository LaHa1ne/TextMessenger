function openCreateChatModalForm() {
    const createChatModalFormSection = $('.create-chat-modal-form-section');
        $.ajax({
            url: '/Chats/CreateChat',
            type: 'GET',
            success: function (response) {
                createChatModalFormSection.html(response);

                createChatModalForm = $('#CreateChatModalForm');
                $(createChatModalForm).find('.btn-primary').click(function () {
                    createChaForm = $('#createChatForm');
                    CreateChat(createChaForm);
                });
                createChatModalForm.modal('show');
            }
        });
}

function CreateChat(createChatForm) {
    if ($(createChatForm).valid()) {
        var formData = $(createChatForm).serialize();

        $.ajax({
            url: '/Chats/CreateChat',
            type: 'POST',
            data: formData,
            success: function (response) {
                $('#CreateChatModalForm').modal('hide');
            },
            error: function (xhr, status, error) {
                // Обработка ошибок, если необходимо
                console.error(xhr.responseText);
            }
        });
    }
}