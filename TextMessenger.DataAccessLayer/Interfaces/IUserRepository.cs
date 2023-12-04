using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsUserExistsWithSelectedNickname(string nickname);
        Task<bool> IsUserExistsWithSelectedEmail(string email);
        Task<User> GetUserWithEmailAndHashPassword(string email, string hashPassword);
        Task<User> GetUserByNickname(string nickname);
        Task<User> GetUserByUserId(Guid userId);
        Task<User> GetUserWithFriendsAndFriendshipSendersByUserId(Guid userId);
        Task<User> GetUserWithFriendsAndFriendshipSendersByNickname(string nickname);
        Task<List<User>> GetUsersByIds(ICollection<Guid> userIds);
    }
}
