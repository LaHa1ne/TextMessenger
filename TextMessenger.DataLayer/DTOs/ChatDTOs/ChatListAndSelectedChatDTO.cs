using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.DTOs.ChatDTOs
{
    public class ChatListAndSelectedChatDTO
    {
        public List<ChatListElementDTO> ChatList { get; set; } = null!;
        public SelectedChatDTO? SelectedChat { get; set; }
    }
}
