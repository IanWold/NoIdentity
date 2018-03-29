using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoIdentity.AuthManager
{
    public class AuthManager
    {
        ClaimsIdentity _identity;

        HttpContext _context;

        public AuthManager(HttpContext context)
        {
            _context = context;
            _identity = new ClaimsIdentity(new Claim[] { }, CookieScheme);
        }

        public AuthManager(HttpContext context, params (string type, string value)[] claims) : this(context) =>
            _identity.AddClaims(claims.Select(c => new Claim(c.type, c.value)));

        public async Task SignInAsync(AuthenticationProperties properties) =>
            await _context.SignInAsync(CookieScheme, new ClaimsPrincipal(_identity), properties);

        public async Task SignOutAsync() =>
            await _context.SignOutAsync(CookieScheme);

        public static async Task SignOutAsync(HttpContext context) =>
            await context.SignOutAsync(CookieScheme);

        public static string CookieScheme =>
            CookieAuthenticationDefaults.AuthenticationScheme;
    }
}
