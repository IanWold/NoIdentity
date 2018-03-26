using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace NoIdentity.Controllers
{
    /// <summary>
    /// This Autheorize attribute requires that:
    ///     A user is logged in, and
    ///     They have the role "Administrator"
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Users()
        {
            return View(Business.User.GetAllByRole(0).ToList());
        }
    }
}