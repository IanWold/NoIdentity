using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoIdentity.Business;
using NoIdentity.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoIdentity.Controllers
{
    public class AuthenticationController : Controller
    {
        // For .NET Framework - can encapsulate long call in function:
        // static  AuthManager() => Request.GetOwinContext().Authentication;

        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Log our user in with cookies.
        /// 
        /// Thanks to:
        ///     https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
        ///     https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
        ///     
        /// And for the .NET Framework requirements:
        ///     https://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
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
                // Validate model state
                // Get user matching provided login credentials
                // Get role for user
                if (ModelState.IsValid
                    && Business.User.GetByUsername(model.Username) is User user
                    && user.Password == model.Password
                    && Role.GetById(user.RoleId) is Role role
                    && role.Id > -1)
                {
                    // Construct a list of claims to log the user in with. This data is stored in session
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("LastModifiedDate", user.LastModifiedDate.ToString())
                    };

                    // Need to create a ClaimsIdentity specifying the cookie schema (from Startup.cs)
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties()
                        {
                            IsPersistent = false
                        }
                    );

                    /* If you want to use my custom AuthManager class to manage all of the above:
                     * 
                     * await new AuthManager.AuthManager(HttpContext, new[] {
                     *     (ClaimTypes.Name, user.FullName),
                     *     (ClaimTypes.NameIdentifier, user.Username),
                     *     ("Id", user.Id.ToString()),
                     *     ("LastModifiedDate", user.LastModifiedDate.ToString()),
                     *     (ClaimTypes.Role, role.Name)
                     * }).SignInAsync(new AuthenticationProperties() {
                     *     IsPersistent = false
                     * });
                     */

                    /* If you are using .NET Framework, use the following alternate code to sign in:
                        * 
                        * var context = Request.GetOwinContext();
                        * var authManager = context.Authentication;
                        *
                        * authManager.SignIn(
                        *     new AuthenticationProperties()
                        *     {
                        *         IsPersistent = false
                        *     },
                        *     identity
                        * );
                        * 
                        * // No problem with the following, I figure (see static method at top of file):
                        * // AuthManager().SignIn(
                        * //     new AuthenticationProperties()
                        * //     {
                        * //         IsPersistent = false
                        * //     },
                        * //     identity
                        * // );
                        */

                    // This can be specified when you register cookie authentication in Startup.cs
                    return RedirectToAction("Index", "Home");
                }
                else return RedirectToAction("Login", "Authentication");

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
            // If using the AuthManager project :
            // await HttpContext.SignOutAsync(AuthManager.AuthManager.CookieScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            /* If you are using .NET Framework, use the following alternate code to sign out:
             * 
             * var context = Request.GetOwinContext();
             * var authManager = context.Authentication;
             *
             * authManager.SignOut("ApplicationCookie");
             * 
             * // No problem with the following, I figure (see static method at top of file):
             * // AuthManager().SignOut("ApplicationCookie");
             */

            // This can also be specified when you register cookie authentication in Startup.cs
            return RedirectToAction("Login", "Authentication");
        }
    }
}
