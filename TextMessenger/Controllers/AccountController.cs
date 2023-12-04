using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextMessenger.DataLayer.DTOs.AuthorizationDTOs;
using TextMessenger.Models;
using TextMessenger.Services.Interfaces;

namespace TextMessenger.Controllers
{
    public class AccountController : Controller
    {
        protected readonly IAccountService _accountService;
        protected readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CheckIsNicknameFree(string nickname)
        {
            return Json(await _accountService.CheckIsNicknameFree(nickname));
        }

        [HttpGet]
        public async Task<IActionResult> CheckIsEmailFree(string email)
        {
            return Json(await _accountService.CheckIsEmailFree(email));
        }

        [HttpPost]
        public async Task<IActionResult> CheckIsEmailAndPasswordCorrect(string email, string password)
        {
            return Json(await _accountService.CheckIsEmailAndPasswordCorrect(email, password));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(_mapper.Map<LoginDTO>(loginViewModel));
                if (response.Data != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", controllerName: "Home");
                }
                ModelState.AddModelError(key: "", errorMessage: response.Description);
            }
            return PartialView("AuthorizationModalForms/LoginModalForm", loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Registration(_mapper.Map<RegistrationDTO>(registrationViewModel));
                if (response.Data != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", controllerName: "Home");
                }
                ModelState.AddModelError(key: "", errorMessage: response.Description);
            }
            return PartialView("AuthorizationModalForms/RegistrationModalForm", registrationViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", controllerName: "Home");
        }

    }
}
