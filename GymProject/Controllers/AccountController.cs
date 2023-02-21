using GymProject.Domain.Helpers;
using GymProject.Domain.ViewModels.Account;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymProject.Controllers
{
    
    public class AccountController : Controller
    {
        private static string Value { get; set; }
        private static string MailUser { get; set; }

        private static string UserLogin { get; set; }
        private readonly IAccountUserService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public AccountController(IAccountUserService accountService, ILogger<AccountController> logger, IEmailService emailService, IUserService userService)
        {
            _accountService = accountService;
            _logger = logger;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                var user = new IdentityUser { UserName = model.Name, Email = model.Email };
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    MailUser = model.Email;
                    UserLogin = model.Name;
                    return RedirectToAction("EmailCheck", "Account");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        }


        public async Task<IActionResult> Save(EmailConfirm email)
        {
            if (email.Value.ToString() == Value)
            {
                var response = await _accountService.ChangeDateConfirmedEmailFromUser(UserLogin);
                if (response)
                    return RedirectToAction("info", "profile");
                else return RedirectToAction("membership", "getmembership");
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EmailCheck()
        {
            Value = MailCodeHelpers.MailCode();
            var response = _emailService.ConfirmEmail(Value, MailUser);
            if (response == true)
                return View();
            else
            return RedirectToAction("emailcheck", "account");
        }

        [HttpPost]
        public async Task<IActionResult> EmailCheck(EmailConfirm email)
        {
            if (email.Value.ToString() == Value) 
            {
                var response = await _accountService.ChangeDateConfirmedEmailFromUser(UserLogin);
                if (response)
                    return RedirectToAction("info", "profile");
                else return RedirectToAction("emailcheck", "account");
            }
            else return RedirectToAction("emailcheck", "account");
        }
    }
}
