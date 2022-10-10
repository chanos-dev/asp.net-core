using JWT.Model;
using JWT.Utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace JWT.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtAuthSettingModel _jwtAuthSettingModel;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtAuthSettingModel> appSettings)
        {
            this._next = next;
            this._jwtAuthSettingModel = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            // string? token = context.Request.Cookies["AccessToken"];

            string? token = context.Request.Headers["Authorization"]
                            .FirstOrDefault()?
                            .Split(_jwtAuthSettingModel.AuthTokenStartName)
                            .Last();
            
            if (token is not null)
            {
                int? idUser = jwtUtils.AccessTokenValidate(token);

                if (idUser.HasValue && idUser.Value > 0)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("idUser", idUser.ToString()!),
                    };

                    ClaimsIdentity identity = new(claims);
                    context.User.AddIdentity(identity);
                }
            }

            await _next(context);
        }
    }
}
