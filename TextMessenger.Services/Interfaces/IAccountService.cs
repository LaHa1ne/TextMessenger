using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.DataLayer.Responses;

namespace TextMessenger.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CheckIsNicknameFree(string nickname);
        Task<bool> CheckIsEmailFree(string nickname);
        Task<bool> CheckIsEmailAndPasswordCorrect(string email, string password);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginDTO loginDTO);
        Task<BaseResponse<ClaimsIdentity>> Registration(RegistrationDTO registrationDTO);
    }
}
