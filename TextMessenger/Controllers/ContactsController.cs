using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Security.Claims;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.Models;
using TextMessenger.Services.Interfaces;
using TextMessenger.Services.Services;

namespace TextMessenger.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        protected readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.GetUserFriends(userId);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetFriendList()
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.GetUserFriends(userId);
            return PartialView("_FriendListPartial", response.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFriend([FromQuery] Guid friendId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.DeleteFriend(userId, friendId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpGet]
        public async Task<IActionResult> GetFriendshipSenderList()
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.GetUserFriendshipSenders(userId);
            return PartialView("_FriendshipSenderListPartial", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptFriendshipRequest([FromQuery] Guid senderId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.AcceptFriendshipRequest(userId, senderId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> RejectFriendshipRequest([FromQuery] Guid senderId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.RejectFriendshipRequest(userId, senderId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpGet]
        public IActionResult GetSendFriendshipRequestForm()
        {
            return PartialView("_SendFriendshipRequestFormPartial");
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendshipRequest(UserNicknameViewModel userNicknameViewModel)
        {
            if (ModelState.IsValid)
            {
                var senderId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
                var response = await _contactsService.SendFriendshipRequest(userNicknameViewModel.Nickname, senderId);
                return new JsonResult(response) { StatusCode = (int)response.StatusCode };
            }
            return PartialView("_SendFriendshipRequestFormPartial", userNicknameViewModel);
        }
    }
}
