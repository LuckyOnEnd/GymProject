using GymProject.DAL.Interfaces;
using GymProject.Domain.ViewModels.Membership;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymProject.Controllers
{
    public class MembershipController : Controller
    {

        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

       [HttpGet]
        public IActionResult GetMembership()
        {
            var respone = _membershipService.GetMemberships();
            return View(respone.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _membershipService.GetMembership(id);
                return PartialView(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(MembershipViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _membershipService.Create(model, imageData);
                }
                else 
                {
                    await _membershipService.Edit(model.Id, model);
                }
                return RedirectToAction("GetMembership");
            }
            return View();
        }
    }
}
