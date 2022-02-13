using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Services
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public CustomCookieAuthenticationEvents()
        {

        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincicap = context.Principal;

            var claim = userPrincicap.Claims.Where(c => c.Type == "LastCheckDateTime").FirstOrDefault();
            var lastCheckDateTime = DateTime.ParseExact(claim.Value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            int interval = 15;

            if (lastCheckDateTime.AddMinutes(interval) < DateTime.UtcNow)
            {
                // 정상 사용자 인지 검증
                if (true)
                {
                    var identity = userPrincicap.Identity as ClaimsIdentity;
                    identity.RemoveClaim(claim);
                    identity.AddClaim(new Claim("LastCheckDateTime", DateTime.UtcNow.ToString("yyyyMMddHHmmss")));

                    await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincicap);                    
                }
                else
                {
                    context.RejectPrincipal();

                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }
    }
}
