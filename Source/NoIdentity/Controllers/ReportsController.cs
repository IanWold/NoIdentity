using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace NoIdentity.Controllers
{
    /// <summary>
    /// The Authorize attribute requires that a user is logged in
    /// </summary>
    [Authorize]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}