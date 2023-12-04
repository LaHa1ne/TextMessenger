
using TextMessenger.DataLayer.Enums;

namespace TextMessenger.DataLayer.DTOs.MessageDTOs
{
    public class ReceivedMessageDTO
    {
        public string Text { get; set; } = null!;
        public int ChatId { get; set; }

    }
}
