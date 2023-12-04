using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.Models;
using TextMessenger.Services.Interfaces;
using TextMessenger.Services.Services;
using AutoMapper;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace TextMessenger.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        protected readonly IHubContext<ChatHub> _hubContext;
        protected readonly IChatsService _chatsService;
        protected readonly IContactsService _contactsService;
        protected readonly IMapper _mapper;

        public ChatsController(IHubContext<ChatHub> hubContext, IChatsService chatsService, IContactsService contactsService, IMapper mapper)
        {
            _hubContext = hubContext;
            _chatsService = chatsService;
            _contactsService = contactsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Chats(int? chatId, Guid? friendId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _chatsService.GetUserChatListWithSelectedChat(userId, chatId, friendId);
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return View(response.Data);
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> GetMoreMessages(int chatId, int firstLoadedMessageId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _chatsService.GetMoreMessages(userId, chatId, firstLoadedMessageId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> GetSelectedChat(int chatId)
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _chatsService.GetSelectedChat(userId, chatId);
            return new JsonResult(response) { StatusCode = (int)response.StatusCode };
        }

        [HttpGet]
        public async Task<IActionResult> CreateChat()
        {
            var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _contactsService.GetUserFriends(userId);
            return PartialView("CreateChatModalForm", _mapper.Map<NewChatViewModel>(response.Data));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(NewChatViewModel newChatViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
                var response = await _chatsService.CreateChat(userId, _mapper.Map<NewChatDTO>(newChatViewModel));
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    await _hubContext.Clients.Users(response.Data.ChatMemberIds).SendAsync("ReceiveMessage", response.Data.message);
                    return Ok();
                }
            }
            return PartialView("CreateChatModalForm", newChatViewModel);
        }
    }
}
