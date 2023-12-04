using AutoMapper;
using TextMessenger.DataLayer.DTOs.MessageDTOs;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.MapperProfiles.MessageProfiles
{
    public class ReceivedMessageDTOToMessageMapperProfile : Profile
    {
        public ReceivedMessageDTOToMessageMapperProfile()
        {
            CreateMap<ReceivedMessageDTO, Message>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));
        }
    }
}