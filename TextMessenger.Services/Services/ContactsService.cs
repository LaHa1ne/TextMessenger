using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Responses;
using TextMessenger.Services.Interfaces;

namespace TextMessenger.Services.Services
{
    public class ContactsService : IContactsService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public ContactsService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<List<UserMainInfoDTO>>> GetUserFriends(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(userId);
                if (user == null)
                {
                    return new BaseResponse<List<UserMainInfoDTO>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new BaseResponse<List<UserMainInfoDTO>>()
                {
                    Description = "Список друзей получен",
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<List<UserMainInfoDTO>>(user.Friends)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserMainInfoDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteFriend(Guid userId, Guid friendId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(userId);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var friend = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(friendId);
                if (friend == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Друг пользователя не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                user.Friends.Remove(friend);
                await _userRepository.Update(user);

                friend.Friends.Remove(user);
                await _userRepository.Update(friend);

                return new BaseResponse<bool>()
                {
                    Description = "Пользователь удален из друзей",
                    Data = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    Data = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<UserMainInfoDTO>>> GetUserFriendshipSenders(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(userId);
                if (user == null)
                {
                    return new BaseResponse<List<UserMainInfoDTO>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new BaseResponse<List<UserMainInfoDTO>>()
                {
                    Description = "Список заявок получен",
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<List<UserMainInfoDTO>>(user.FriendshipSenders)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserMainInfoDTO>>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> AcceptFriendshipRequest(Guid userId, Guid senderId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(userId);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var sender = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(senderId);
                if (sender == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Отправитель заявки не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                user.FriendshipSenders.Remove(sender);
                user.Friends.Add(sender);
                await _userRepository.Update(user);

                sender.FriendshipSenders.Remove(user);
                sender.Friends.Add(user);
                await _userRepository.Update(sender);

                return new BaseResponse<bool>()
                {
                    Description = "Запрос в друзья принят",
                    Data = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    Data = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> RejectFriendshipRequest(Guid userId, Guid senderId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(userId);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var sender = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(senderId);
                if (sender == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Отправитель заявки не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                user.FriendshipSenders.Remove(sender);
                await _userRepository.Update(user);

                if (sender.FriendshipSenders.Contains(user))
                {
                    sender.FriendshipSenders.Remove(user);
                    await _userRepository.Update(sender);
                }

                return new BaseResponse<bool>()
                {
                    Description = "Запрос в друзья отклонен",
                    Data = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> SendFriendshipRequest(string nickname, Guid senderId)
        {
            try
            {
                var user = await _userRepository.GetUserWithFriendsAndFriendshipSendersByNickname(nickname);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var sender = await _userRepository.GetUserWithFriendsAndFriendshipSendersByUserId(senderId);
                if (sender == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Отправитель заявки не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                if (sender.Friends.Contains(user) || user.UserId == sender.UserId)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь уже у вас в друзьях",
                        Data = false,
                        StatusCode = HttpStatusCode.OK
                    };
                }

                if (sender.FriendshipSenders.Contains(user))
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "От данного пользователя уже поступил запрос дружбы",
                        Data = false,
                        StatusCode = HttpStatusCode.OK
                    };
                }

                user.FriendshipSenders.Add(sender);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Description = "Запрос в друзья отправлен",
                    Data = true,
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
