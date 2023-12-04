using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TextMessenger.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
