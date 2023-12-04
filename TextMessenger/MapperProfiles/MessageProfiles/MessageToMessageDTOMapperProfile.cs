using AutoMapper;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Helpers;

namespace TextMessenger.MapperProfiles.MessageProfiles
{
    public class MessageToMessageDTOMapperProfile : Profile
    {
        public MessageToMessageDTOMapperProfile()
        {
            CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateConvertHelper.ConvertDateToString(src.Date)))
                .ForMember(dest => dest.IsSystem, opt => opt.MapFrom(src => src.IsSystem))
                .ForMember(dest => dest.MessageCreatorId, opt => opt.MapFrom(src => src.MessageCreatorId))
                .ForMember(dest => dest.UserNickname, opt => opt.MapFrom(src => src.MessageCreator != null ? src.MessageCreator.Nickname : ""));
        }
    }
}