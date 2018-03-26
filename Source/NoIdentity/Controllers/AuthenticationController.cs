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

                // Add roles into claims
                //var roles = _roleService.GetByUserId(user.Id);
                //if (roles.Any())
                //{
                //    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
                //    identity.AddClaims(roleClaims);
                //}

                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction("Login", "Authentication");
            }
        } 
    }
}