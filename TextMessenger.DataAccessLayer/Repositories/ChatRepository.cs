using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataAccessLayer.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext db) : base(db) { }

        public async Task<bool> CheckIsChatExistsByChatId(int chatId)
        {
            return await _db.Chats.AnyAsync(c => c.ChatId == chatId);
        }
        public async Task<List<Chat>> GetUserChats(Guid userId)
        {
            var res =  await _db.Chats.Where(c => c.ChatMembers.Any(cm => cm.UserId == userId)).Include(c => c.ChatMessages.Where(cm => !cm.IsDeleted).OrderByDescending(cm => cm.MessageId).Take(1))
                .Include(c => c.ChatMembers.Where(cm => cm.UserId != userId).Take(1)).OrderByDescending(c => c.ChatMessages.OrderByDescending(cm => cm.MessageId).First().MessageId)
                .ToListAsync();
            return res;
        }

        public async Task<Chat> GetChatWithChatMessagesAndChatMembersByChatId(int chatId, int messageCount)
        {
            return await _db.Chats.Include(c => c.ChatMessages.Where(cm => !cm.IsDeleted).OrderByDescending(cm => cm.MessageId).Take(messageCount)).Include(c => c.ChatMembers).FirstOrDefaultAsync(c => c.ChatId == chatId);
        }

        public async Task<Chat> GetPersonalChatWithSelectedUsers(Guid firstUserId, Guid secondUserId)
        {
            return await _db.Chats.Include(c => c.ChatMembers)
                .Where(c => c.Type == ChatType.Personal && c.ChatMembers.Any(cm => cm.UserId == firstUserId) && c.ChatMembers.Any(cm => cm.UserId == secondUserId)).FirstOrDefaultAsync();
        }

        public async Task<Chat> GetChatWithChatMembersByChatId(int chatId)
        {
            return await _db.Chats.Include(c => c.ChatMembers).FirstOrDefaultAsync(c => c.ChatId == chatId);
        }
    }
}
