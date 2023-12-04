using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.DataLayer.Entities;
using TextMessenger.DataLayer.Helpers;
using TextMessenger.DataLayer.Responses;
using TextMessenger.Services.Interfaces;

namespace TextMessenger.Services.Services
{
    public class AccountService : IAccountService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public AccountService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckIsNicknameFree(string nickname)
        {
            try
            {
                return !await _userRepository.IsUserExistsWithSelectedNickname(nickname);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckIsEmailFree(string email)
        {
            try
            {
                return !await _userRepository.IsUserExistsWithSelectedEmail(email);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckIsEmailAndPasswordCorrect(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetUserWithEmailAndHashPassword(email, HashPasswordHelper.GetHashPassword(password));
                return user != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _userRepository.GetUserWithEmailAndHashPassword(loginDTO.Email, HashPasswordHelper.GetHashPassword(loginDTO.Password));
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Вход в аккаунт совершен",
                    StatusCode = HttpStatusCode.OK,
                    Data = Authenticate(user)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Внутренняя ошибка сервера",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Registration(RegistrationDTO registrationDTO)
        {
            try
            {
                var user = _mapper.Map<User>(registrationDTO);

                await _userRepository.Create(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = Authenticate(user),
                    Description = "Аккаунт создан",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        private static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };
            return new ClaimsIdentity(claims, authenticationType: "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
