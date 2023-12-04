using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.DTOs.MessageDTOs
{
    public class MessageListDTO
    {
        public Guid UserId { get; set; }
        public List<MessageDTO> Messages { get; set; } = null!;
        public bool HasMoreMessages { get; set; }
    }
}
