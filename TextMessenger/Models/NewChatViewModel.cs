using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TextMessenger.DataLayer.DTOs.UserDTOs;

namespace TextMessenger.Models
{
    public class NewChatViewModel
    {
        [Required(ErrorMessage = "Введите название чата")]
        [UIHint("ChatName")]
        [Display(Name = "Название чата")]
        public string ChatName { get; set; } = null!;

        [UIHint("SelectedFriends")]
        [Display(Name = "Участники")]
        [MinLength(2, ErrorMessage = "Выберите минимум двух друзей")]
        public HashSet<Guid> SelectedFriends { get; set; } = null!;

        public List<Guid> FriendsIds { get; set; } = null!;
        public List<string> FriendsNicknames { get; set; } = null!;

    }
}
