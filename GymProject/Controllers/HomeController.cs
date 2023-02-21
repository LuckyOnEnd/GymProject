using GymProject.DAL.Interfaces;
using GymProject.DAL.Repositories;
using GymProject.Domain.Entity;
using GymProject.Domain.ViewModels.Account;
using GymProject.Models;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GymProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> ContactEmail(EmailViewModels models)
        {
                var response = _emailService.ContactEmailSend(models.Text, models.Email, models.Name);
                if (response)
                    return RedirectToAction("info", "profile");
                else return RedirectToAction("membership", "getmembership");
            
        }
    }
}
