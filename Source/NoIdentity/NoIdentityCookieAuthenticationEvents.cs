using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NoIdentity.Business;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoIdentity
{
    /// <summary>
    /// Capture cookie authentication events (self-explanatory name)
    /// </summary>
    public class NoIdentityCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        /// <summary>
        /// Suppose you persist the cookie across browser sessions but you need (or want) to sign the user out in certain conditions.
        /// This signs them out if their data in the DB has been updated since they last logged in.
        /// This specific case might be redundant but it shows how to do this.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            
            var lastChanged = userPrincipal.Claims.Where(c => c.Type == "LastModifiedDate").Select(c => c.Value).FirstOrDefault();
            var username = userPrincipal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(lastChanged) || User.GetByUsername(username).LastModifiedDate.ToString() != lastChanged)
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
