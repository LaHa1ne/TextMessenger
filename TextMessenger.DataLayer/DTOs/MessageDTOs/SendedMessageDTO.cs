using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataLayer.DTOs.MessageDTOs
{
    public class SendedMessageDTO
    {
        public int MessageId { get; set; }
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Date { get; set; } = null!;
        public int ChatId { get; set; }
        public string ChatName { get; set; } = null!;
        public ChatType Type { get; set; }
        public bool IsSystem { get; set; }

    }
}
