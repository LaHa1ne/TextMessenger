using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataLayer.Entities
{
    public class Chat
    {
        public Chat()
        {
            ChatMembers = new List<User>();
            ChatMessages = new List<Message>();
        }
        public int ChatId { get; set; }
        public string Name { get; set; } = null!;
        public ChatType Type { get; set; }
        public Guid ChatCreatorId { get; set; }
        public User ChatCreator { get; set; } = null!;
        public virtual ICollection<User> ChatMembers { get; set; }
        public virtual ICollection<Message> ChatMessages { get; set; }
    }
}
