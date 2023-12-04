using AutoMapper;
using TextMessenger.DataLayer.DTOs.UserDTOs;
using TextMessenger.DataLayer.Entities;

namespace TextMessenger.MapperProfiles.UserProfiles
{
    public class UserToUserMainInfoDTOMapperProfile : Profile
    {
        public UserToUserMainInfoDTOMapperProfile()
        {
            CreateMap<User, UserMainInfoDTO>();
        }
    }
}
