using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoIdentity.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Authentication;

namespace NoIdentity.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Log our user in.
        /// 
        /// Thanks to:
        ///     https://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
        ///     https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
        /// </summary>
        /// <param name="model"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string redirectUrl)
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

                await HttpContext.SignInAsync("Cookie", new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });

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