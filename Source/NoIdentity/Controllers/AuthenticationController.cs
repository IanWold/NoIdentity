using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoIdentity.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                // Get user matching provided login credentials
                var user = Business.User.GetByUsernameAndPassword(model.Username, model.Password);

                // Construct a list of claims to log the user in with. This data is stored in session
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName)
                };

                // Need to create a ClaimsIdentity specifying the cookie schema (from Startup.cs)
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Demonstrates how to use roles with this approach
                var role = Business.Role.GetById(user.RoleId);
                if (role.Id > -1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }

                // This actually signs the user in
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties()
                    {
                        IsPersistent = true
                    }
                );

                // This can be specified when you register cookie authentication in Startup.cs
                return RedirectToAction("Index", "Reports");
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Login", "Authentication");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        } 
    }
}