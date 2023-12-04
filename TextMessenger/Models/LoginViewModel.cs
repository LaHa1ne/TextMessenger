using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TextMessenger.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите эл. почту")]
        [StringLength(50, ErrorMessage = "Длина эл. почты не может превышать 50 символов")]
        [EmailAddress(ErrorMessage = "Некорректный формат эл. почты")]
        [UIHint("EmailAddress")]
        [Display(Name = "Эл. почта")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля от 6 до 50 символов")]
        [Remote(action: "CheckIsEmailAndPasswordCorrect", controller: "Account", AdditionalFields = nameof(Email),HttpMethod = "post", ErrorMessage = "Неправильная почта или пароль")]
        [UIHint("Password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;
    }
}
