using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataAccessLayer.Repositories;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Enums;
using TextMessenger.DataLayer.Helpers;
using TextMessenger.DataLayer.Responses;
using TextMessenger.Services.Interfaces;

namespace TextMessenger.Services.Services
{
    public class ChatsService : IChatsService
    {
        protected const int messageCount = 3;
        protected readonly IUserRepository _userRepository;
        protected readonly IChatRepository _chatRepository;
        protected readonly IMessageRepository _messageRepository;
        protected readonly IMapper _mapper;

        public ChatsService(IUserRepository userRepository, IChatRepository chatRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ChatListAndSelectedChatDTO>> GetUserChatListWithSelectedChat(Guid userId, int? chatId, Guid? friendId)
        {
            try
            {
                if (friendId != null)
                {
                    var chat = await _chatRepository.GetPersonalChatWithSelectedUsers(userId, friendId.Value);
                    if (chat == null)
                    {
                        chat = new Chat()
                        {
                            Name = "Личный чат",
                            Type = ChatType.Personal,
                            ChatCreatorId = userId
                        };
                        chat.ChatMembers.Add(await _userRepository.GetUserByUserId(userId));
                        chat.ChatMembers.Add(await _userRepository.GetUserByUserId(friendId.Value));
                        chat.ChatMessages.Add(new Message()
                        {
                            Text = "Чат создан",
                            Date = DateTimeOffset.UtcNow,
                            IsSystem = true,
                            IsDeleted = false
                        });
                        await _chatRepository.Create(chat);
                    }
                    chatId = chat.ChatId;
                }

                var chatList = _mapper.Map<List<ChatListElementDTO>>(await _chatRepository.GetUserChats(userId));
                chatList.ForEach(chat => {chat.ChatName = chat.Type == ChatType.Personal ? chat.ChatMembers.First(cm => cm.UserId != userId).Nickname : chat.ChatName;});
                var selectedChat = chatId == null ? null : _mapper.Map<SelectedChatDTO>(await _chatRepository.GetChatWithChatMessagesAndChatMembersByChatId(chatId.Value, messageCount));
                if (selectedChat != null)
                {
                    selectedChat.UserId = userId;
                    if (selectedChat.Type == ChatType.Personal) { selectedChat.Name = selectedChat.ChatMembers.First(cm => cm.UserId != userId).Nickname; }
                }

                return new BaseResponse<ChatListAndSelectedChatDTO>()
                {
                    Description = "Список чатов и выбранный чат получены",
                    StatusCode = HttpStatusCode.OK,
                    Data = new ChatListAndSelectedChatDTO()
                    {
                        ChatList = chatList,
                        SelectedChat = selectedChat
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatListAndSelectedChatDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<MessageListDTO>> GetMoreMessages(Guid userId, int chatId, int firstLoadedMessageId)
        {
            try
            {
                if (!await _chatRepository.CheckIsChatExistsByChatId(chatId))
                {
                    return new BaseResponse<MessageListDTO>()
                    {
                        Description = "Выбранный чат не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var messages = await _messageRepository.GetMessagesBeforeSelected(chatId, firstLoadedMessageId, messageCount);
                return new BaseResponse<MessageListDTO>()
                {
                    Description = "Сообщения получены",
                    StatusCode = HttpStatusCode.OK,
                    Data = new MessageListDTO()
                    {
                        UserId = userId,
                        HasMoreMessages = messages.Count == messageCount,
                        Messages = _mapper.Map<List<MessageDTO>>(messages)
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MessageListDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<SelectedChatDTO>> GetSelectedChat(Guid userId, int chatId)
        {
            try
            {
                if (!await _chatRepository.CheckIsChatExistsByChatId(chatId))
                {
                    return new BaseResponse<SelectedChatDTO>()
                    {
                        Description = "Выбранный чат не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var selectedChat = _mapper.Map<SelectedChatDTO>(await _chatRepository.GetChatWithChatMessagesAndChatMembersByChatId(chatId, messageCount));
                if (selectedChat != null)
                {
                    selectedChat.UserId = userId;
                    if (selectedChat.Type == ChatType.Personal) { selectedChat.Name = selectedChat.ChatMembers.First(cm => cm.UserId != userId).Nickname; }
                }

                return new BaseResponse<SelectedChatDTO>()
                {
                    Description = "Выбранный чат получен",
                    StatusCode = HttpStatusCode.OK,
                    Data = selectedChat
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SelectedChatDTO>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>> AddReceivedMessage(Guid senderId, ReceivedMessageDTO receivedMessage)
        {
            try
            {
                var chat = await _chatRepository.GetChatWithChatMembersByChatId(receivedMessage.ChatId);
                if (chat == null)
                {
                    return new BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>()
                    {
                        Description = "Чат не найден",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var newMessage = _mapper.Map<Message>(receivedMessage);
                newMessage.MessageCreatorId = senderId;
                await _messageRepository.Create(newMessage);

                var sendedMessage = new SendedMessageDTO()
                {
                    MessageId = newMessage.MessageId,
                    UserId = senderId,
                    Nickname = chat.ChatMembers.First(cm => cm.UserId == senderId).Nickname,
                    Text = newMessage.Text,
                    Date = DateConvertHelper.ConvertDateToString(newMessage.Date),
                    ChatId = chat.ChatId,
                    Type = chat.Type,
                    IsSystem = newMessage.IsSystem
                };
                var chatMembersIds = chat.ChatMembers.Select(cm => cm.UserId.ToString()).ToList();

                return new BaseResponse<(SendedMessageDTO message, List<string> chatMemberIds)>()
                {
                    Description = "Сообщение сохранено",
                    StatusCode = HttpStatusCode.OK,
                    Data = (sendedMessage, chatMembersIds)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<(SendedMessageDTO message, List<string> chatMemberIds)>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>> CreateChat(Guid userId, NewChatDTO newChatDTO)
        {
            try
            {
                newChatDTO.ChatMembersIds.Add(userId);
                var newChat = new Chat()
                {
                    Name = newChatDTO.ChatName,
                    Type = ChatType.Group,
                    ChatCreatorId = userId
                };

                foreach (var chatMember in await _userRepository.GetUsersByIds(newChatDTO.ChatMembersIds))
                {
                    newChat.ChatMembers.Add(chatMember);
                }

                var firstChatMessage = new Message()
                {
                    Text = "Чат создан",
                    Date = DateTimeOffset.UtcNow,
                    IsSystem = true,
                    IsDeleted = false
                };
                newChat.ChatMessages.Add(firstChatMessage);
                await _chatRepository.Create(newChat);

                var sendedMessage = new SendedMessageDTO()
                {
                    MessageId = firstChatMessage.MessageId,
                    Nickname = "",
                    Text = firstChatMessage.Text,
                    Date = DateConvertHelper.ConvertDateToString(firstChatMessage.Date),
                    ChatId = newChat.ChatId,
                    ChatName = newChat.Name,
                    Type = newChat.Type,
                    IsSystem = firstChatMessage.IsSystem
                };
                var chatMembersIds = newChat.ChatMembers.Select(cm => cm.UserId.ToString()).ToList();

                return new BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>()
                {
                    Description = "Чат создан",
                    Data = (sendedMessage, chatMembersIds),
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<(SendedMessageDTO message, List<string> ChatMemberIds)>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}