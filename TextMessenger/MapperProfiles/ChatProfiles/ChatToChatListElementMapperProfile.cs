using AutoMapper;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Enums;
using TextMessenger.DataLayer.Helpers;

namespace TextMessenger.MapperProfiles.ChatProfiles
{
    public class ChatToChatListElementMapperProfile : Profile
    {
        public ChatToChatListElementMapperProfile() 
        {
            CreateMap<Chat, ChatListElementDTO>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.Type == ChatType.Personal ? src.ChatMembers.First().Nickname : src.Name))
                .ForMember(dest => dest.LastMessageDate, opt => opt.MapFrom(src => src.ChatMessages.Count == 0 ? "" : DateConvertHelper.ConvertDateToString(src.ChatMessages.First().Date)))
                .ForMember(dest => dest.LastMessageText, opt => opt.MapFrom(src => src.ChatMessages.Count == 0 ? "" : StringTruncatorHelper.TruncateToSingleLine(src.ChatMessages.First().Text)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.ChatMembers, opt => opt.MapFrom(src => src.ChatMembers.Select(cm => new UserMainInfoDTO()
                {
                    UserId = cm.UserId,
                    Nickname = cm.Nickname
                })));
        }
    }
}