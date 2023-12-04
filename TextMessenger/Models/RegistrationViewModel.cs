using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TextMessenger.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Введите никнейм")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Длина никнейма от 4 до 16 символов")]
        [Remote(action: "CheckIsNicknameFree", controller: "Account", ErrorMessage = "Никнейм уже занят")]
        [Display(Name = "Никнейм")]
        public string Nickname { get; set; } = null!;

        [Required(ErrorMessage = "Введите эл. почту")]
        [StringLength(50, ErrorMessage = "Длина эл. почты не может превышать 50 символов")]
        [EmailAddress(ErrorMessage = "Некорректный формат эл. почты")]
        [Remote(action: "CheckIsEmailFree", controller: "Account", ErrorMessage = "Эл. почта уже занята")]
        [UIHint("EmailAddress")]
        [Display(Name = "Эл. почта")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля от 6 до 50 символов")]
        [UIHint("Password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [UIHint("Password")]
        [Display(Name = "Подтверждение пароля")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
