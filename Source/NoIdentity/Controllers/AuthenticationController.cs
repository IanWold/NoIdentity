using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoIdentity.ViewModels;
using System.Web;
using Microsoft.Owin.Security;

namespace NoIdentity.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Thanks to: [SO link]
        /// </summary>
        /// <param name="model"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string redirectUrl)
        {
            try
            {
                var user = Business.User.GetByUsernameAndPassword(model.Username, model.Password);

                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName)
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                var role = Business.Role.GetById(user.RoleId);
                if (role.Id > -1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                //Still broken:
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Login", "Authentication");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        } 
    }
}