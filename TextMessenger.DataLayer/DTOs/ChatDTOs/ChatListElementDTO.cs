using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataLayer.DTOs.ChatDTOs
{
    public class ChatListElementDTO
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; } = null!;
        public string LastMessageDate { get; set; } = null!;
        public string LastMessageText { get; set; } = null!;
        public ChatType Type { get; set; }
        public List<UserMainInfoDTO> ChatMembers { get; set; } = null!;

    }
}
