using AutoMapper;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.Models;

namespace TextMessenger.MapperProfiles.UserProfiles
{
    public class UserMainInfoDTOListToNewChatViewModelMapperProfile : Profile
    {
        public UserMainInfoDTOListToNewChatViewModelMapperProfile()
        {
            CreateMap<List<UserMainInfoDTO>, NewChatViewModel>()
                 .ForMember(dest => dest.FriendsIds, opt => opt.MapFrom(src => src.Select(u => u.UserId)))
                 .ForMember(dest => dest.FriendsNicknames, opt => opt.MapFrom(src => src.Select(u => u.Nickname)))
                 .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => ""))
                 .ForMember(dest => dest.SelectedFriends, opt => opt.MapFrom(src => new HashSet<Guid>()));
        }
    }
}
