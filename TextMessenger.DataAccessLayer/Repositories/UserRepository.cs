using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db) { }
        public async Task<bool> IsUserExistsWithSelectedNickname(string nickname)
        {
            return await _db.Users.AnyAsync(u => u.Nickname == nickname);
        }
        public async Task<bool> IsUserExistsWithSelectedEmail(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<User> GetUserWithEmailAndHashPassword(string email, string hashPassword)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashPassword);
        }
        public async Task<User> GetUserByNickname(string nickname)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
        }
        public async Task<User> GetUserByUserId(Guid userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetUserWithFriendsAndFriendshipSendersByUserId(Guid userId)
        {
            return await _db.Users.Include(u => u.Friends).Include(u => u.FriendshipSenders).FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetUserWithFriendsAndFriendshipSendersByNickname(string nickname)
        {
            return await _db.Users.Include(u => u.Friends).Include(u => u.FriendshipSenders).FirstOrDefaultAsync(u => u.Nickname == nickname);
        }

        public async Task<List<User>> GetUsersByIds(ICollection<Guid> userIds)
        {
            return _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
        }
    }
}
