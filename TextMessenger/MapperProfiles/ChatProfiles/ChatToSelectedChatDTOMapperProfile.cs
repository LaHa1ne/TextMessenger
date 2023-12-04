using AutoMapper;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Helpers;

namespace TextMessenger.MapperProfiles.ChatProfiles
{
    public class ChatToSelectedChatDTOMapperProfile : Profile
    {
        public ChatToSelectedChatDTOMapperProfile()
        {
            CreateMap<Chat, SelectedChatDTO>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.ChatMembers, opt => opt.MapFrom(src => src.ChatMembers.Select(cm => new UserMainInfoDTO()
                {
                    UserId = cm.UserId,
                    Nickname = cm.Nickname
                })))
                .ForMember(dest => dest.ChatMessages, opt => opt.MapFrom(src => src.ChatMessages.Select(cm => new MessageDTO()
                {
                    MessageId = cm.MessageId,
                    ChatId = cm.ChatId,
                    Text = cm.Text,
                    Date = DateConvertHelper.ConvertDateToString(cm.Date),
                    IsSystem = cm.IsSystem,
                    MessageCreatorId = cm.MessageCreatorId,
                    UserNickname = cm.MessageCreator == null ? "" : cm.MessageCreator.Nickname
                })));
        }
    }
}