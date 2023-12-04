using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.Entities
{
    public class User
    {
        public User()
        {
            CreatedChats = new List<Chat>();
            Chats = new List<Chat>();
            Messages = new List<Message>();
            Friends = new HashSet<User>();
            FriendsNavigation = new HashSet<User>();
            FriendshipSenders = new HashSet<User>();
            FriendshipSendersNavigation = new HashSet<User>();
        }
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public virtual ICollection<Chat> CreatedChats { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Friends { get; set; }
        public virtual ICollection<User> FriendsNavigation { get; set; }
        public virtual ICollection<User>  FriendshipSenders { get; set; }
        public virtual ICollection<User> FriendshipSendersNavigation { get; set; }
    }
}
