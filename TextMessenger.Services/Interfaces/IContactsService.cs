using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Responses;

namespace TextMessenger.Services.Interfaces
{
    public interface IContactsService
    {
        Task<BaseResponse<List<UserMainInfoDTO>>> GetUserFriends(Guid userId);
        Task<BaseResponse<bool>> DeleteFriend(Guid userId, Guid friendId);
        Task<BaseResponse<List<UserMainInfoDTO>>> GetUserFriendshipSenders(Guid userId);
        Task<BaseResponse<bool>> AcceptFriendshipRequest(Guid userId, Guid senderId);
        Task<BaseResponse<bool>> RejectFriendshipRequest(Guid userId, Guid senderId);
        Task<BaseResponse<bool>> SendFriendshipRequest(string nickname, Guid senderId);
    }
}
