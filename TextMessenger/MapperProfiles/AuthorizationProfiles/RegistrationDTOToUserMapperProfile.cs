using AutoMapper;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Helpers;

namespace TextMessenger.MapperProfiles.AuthorizationProfiles
{
    public class RegistrationDTOToUserMapperProfile : Profile
    {
        public RegistrationDTOToUserMapperProfile()
        {
            CreateMap<RegistrationDTO, User>()
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPasswordHelper.GetHashPassword(src.Password)));
        }
    }
}
