using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NoIdentity.Business;

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
            List<Report> model = new List<Report>();

            model = (User.IsInRole("Administrator")
                ? Business.Report.GetAll().PopulateUserData()
                : Business.Report.GetAllByUser(Convert.ToInt32(User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).First())))
                .ToList();

            return View(model);
        }

        public IActionResult Report(string id)
        {
            try
            {
                return View(Business.Report.GetById(Convert.ToInt32(id)).PopulateUserData());
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reports");
            }
        }
    }
}