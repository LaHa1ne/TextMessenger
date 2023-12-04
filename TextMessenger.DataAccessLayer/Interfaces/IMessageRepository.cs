using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetMessagesBeforeSelected(int chatId, int messageId, int messageCount);
    }
}
