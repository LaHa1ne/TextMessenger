using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataLayer.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; } = null!;
        public string Date { get; set; } = null!;
        public bool IsSystem { get; set; }
        public Guid? MessageCreatorId { get; set; }
        public string UserNickname { get; set; } = null!;
    }
}
