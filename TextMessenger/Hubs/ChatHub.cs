using Microsoft.AspNetCore.SignalR;
using TextMessenger.DataLayer.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.Services.Interfaces;
using System.Net;

namespace TextMessenger.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        protected readonly IChatsService _chatsService;
        public ChatHub(IChatsService chatsService) : base()
        {
            _chatsService = chatsService;
        }
        public async Task SendMessage(ReceivedMessageDTO message)
        {
            var userId = Guid.Parse(Context.User!.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
            var response = await _chatsService.AddReceivedMessage(userId, message);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await Clients.Users(response.Data.ChatMemberIds).SendAsync("ReceiveMessage", response.Data.message);
            }
        }
    }
}
