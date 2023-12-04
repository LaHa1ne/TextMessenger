using AutoMapper;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.Models;

namespace TextMessenger.MapperProfiles.AuthorizationProfiles
{
    public class RegistrationViewModelToRegistrationDTOMapperProfile : Profile
    {
        public RegistrationViewModelToRegistrationDTOMapperProfile()
        {
            CreateMap<RegistrationViewModel, RegistrationDTO>();
        }

    }
}
