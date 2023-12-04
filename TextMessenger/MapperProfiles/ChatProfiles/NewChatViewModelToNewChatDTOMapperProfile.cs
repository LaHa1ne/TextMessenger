using AutoMapper;
using TextMessenger.DataLayer.DTOs.ChatDTOs;
using TextMessenger.Models;

namespace TextMessenger.MapperProfiles.ChatProfiles
{
    public class NewChatViewModelToNewChatDTOMapperProfile : Profile
    {
        public NewChatViewModelToNewChatDTOMapperProfile()
        {
            CreateMap<NewChatViewModel, NewChatDTO>()
                .ForMember(dist => dist.ChatName, opt => opt.MapFrom(src => src.ChatName))
                .ForMember(dist => dist.ChatMembersIds, opt => opt.MapFrom(src => src.FriendsIds));

        }
    }
}
