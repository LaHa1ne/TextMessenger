using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext db) : base(db) { }

        public async Task<List<Message>> GetMessagesBeforeSelected(int chatId, int messageId, int messageCount)
        {
            return await _db.Messages.Include(m => m.MessageCreator).Where(m => m.ChatId == chatId && m.MessageId < messageId && !m.IsDeleted).OrderByDescending(m => m.MessageId).Take(messageCount).ToListAsync();
        }
    }
}
