using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataLayer.DTOs.ChatDTOs
{
    public class SelectedChatDTO
    {
        public Guid UserId { get; set; }
        public int ChatId { get; set; }
        public string Name { get; set; } = null!;
        public ChatType Type { get; set; }
        public List<UserMainInfoDTO> ChatMembers { get; set; } = null!;
        public List<MessageDTO> ChatMessages { get; set; } = null!;
    }
}
