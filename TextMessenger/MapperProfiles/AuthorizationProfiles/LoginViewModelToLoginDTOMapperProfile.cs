using AutoMapper;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.Models;

namespace TextMessenger.MapperProfiles.AuthorizationProfiles
{
    public class LoginViewModelToLoginDTOMapperProfile : Profile
    {
        public LoginViewModelToLoginDTOMapperProfile() 
        {
            CreateMap<LoginViewModel, LoginDTO>();
        }

    }
}
