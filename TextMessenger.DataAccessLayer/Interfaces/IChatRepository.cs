using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<bool> CheckIsChatExistsByChatId(int chatId);
        Task<List<Chat>> GetUserChats(Guid userId);
        Task<Chat> GetChatWithChatMessagesAndChatMembersByChatId(int chatId, int messageCount);
        Task<Chat> GetPersonalChatWithSelectedUsers(Guid firstUserId, Guid secondUserId);
        Task<Chat> GetChatWithChatMembersByChatId(int chatId);
    }
}
