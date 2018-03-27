using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NoIdentity.Business;
using NoIdentity.ViewModels;

namespace NoIdentity.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        public IActionResult Account(int id)
        {
            try
            {
                return View(new ManageAccountViewModel(Business.User.GetById(id)));
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Account(ManageAccountViewModel model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                //Do things
            }

            return RedirectToAction(redirectUrl);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Users()
        {
            return View(Business.User.GetAllByRole(0).ToList());
        }
    }
}