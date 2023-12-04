using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.Responses;

namespace TextMessenger.Services.Interfaces
{
    public interface IChatsService
    {
        Task<BaseResponse<ChatListAndSelectedChatDTO>> GetUserChatListWithSelectedChat(Guid userId, int? chatId, Guid? friendId);

        Task<BaseResponse<MessageListDTO>> GetMoreMessages(Guid userId, int chatId, int firstLoadedMessageId);

        Task<BaseResponse<SelectedChatDTO>> GetSelectedChat(Guid userId, int chatId);

        Task<BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>> AddReceivedMessage(Guid senderId, ReceivedMessageDTO receivedMessage);

        Task<BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>> CreateChat(Guid userId, NewChatDTO newChatDTO);
    }
}
