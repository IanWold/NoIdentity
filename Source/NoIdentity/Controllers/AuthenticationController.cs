﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NoIdentity.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoIdentity.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Log our user in with cookies
        /// 
        /// Thanks to:
        ///     https://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
        ///     https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
        ///     https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
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
                if (Business.User.GetByUsername(model.Username) is Business.User user && user.Password == model.Password)
                {
                    // Construct a list of claims to log the user in with. This data is stored in session
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("LastModifiedDate", user.LastModifiedDate.ToString())
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
                            IsPersistent = false
                        }
                    );

                    // This can be specified when you register cookie authentication in Startup.cs
                    return RedirectToAction("Index", "Home");
                }
                else throw new ArgumentException("Username or Password incorrect.");

            }
            catch (ArgumentException)
            {
                return RedirectToAction("Login", "Authentication");
            }
        }

        /// <summary>
        /// Log the user out
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // This can also be specified when you register cookie authentication in Startup.cs
            return RedirectToAction("Login", "Authentication");
        }
    }
}
