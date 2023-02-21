using GymProject.Domain.Enum;
using GymProject.Domain.ViewModels.Account;
using GymProject.Domain.ViewModels.User;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymProject.Views.Admin
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
    }
}
