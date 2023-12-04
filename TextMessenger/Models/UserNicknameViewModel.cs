using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TextMessenger.Models
{
    public class UserNicknameViewModel
    {
        [Required(ErrorMessage = "Введите никнейм")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Длина никнейма от 4 до 16 символов")]
        public string Nickname { get; set; } = null!;
    }
}
