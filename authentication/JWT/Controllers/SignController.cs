using JWT.AttributeFilter;
using JWT.Model;
using JWT.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SignController : ControllerBase
    {
        private static int USER_IDX = 1;
        private static List<User> MemoryUserTable { get; set; }
        private static List<UserRefreshToken> MemoryRefreshTokenTable { get; set; }

        public readonly string AccessTokenName = "AccessToken";
        public readonly string RefreshTokenName = "RefreshToken";

        private readonly IJwtUtils _jwtUtils;

        static SignController()
        {
            MemoryUserTable = new();
            MemoryRefreshTokenTable = new();
        }

        public SignController(IJwtUtils jwtUtils)
        {
            this._jwtUtils = jwtUtils;
        }

        private void CookieAccessToken(string token)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append(AccessTokenName, token, cookieOptions);
        }

        private void CookieRefreshToken(string token)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append(AccessTokenName, token, cookieOptions);
        }

        [HttpPost]
        public ActionResult<ApiResultModel> Register([FromForm] string signName,
                                                     [FromForm] string password)
        {
            ApiResultModel result = new()
            {
                Complete = true,
            };


            if (string.IsNullOrEmpty(signName))
            {
                result.Message = "signName is required.";
                result.Complete = false;
            }
            else if (string.IsNullOrEmpty(password))
            {
                result.Message = "password is required.";
                result.Complete = false;
            }

            if (result.Complete)
            {
                User? findUser = MemoryUserTable.Where(u => u.SignName == signName).FirstOrDefault();

                if (findUser is null)
                {
                    MemoryUserTable.Add(new User()
                    {
                        IdUser = USER_IDX++,
                        SignName = signName,
                        PasswordHash = password,
                    });
                }
                else
                {
                    result.Message = $"{signName} is already in DB.";
                    result.Complete = false;
                }
            }

            return result;
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult<SignInModel> SignIn([FromForm] string signName,
                                                     [FromForm] string password)
        { 
            User? findUser = MemoryUserTable.Where(u => u.SignName == signName && u.PasswordHash == password).FirstOrDefault();

            if (findUser is null)                
                return BadRequest($"The account is not corret.");

            SignInModel result = new()
            {
                idUser = findUser.IdUser,
                Complete = true,
            };

            string accessToken = this._jwtUtils.AccessTokenGenerate(findUser.IdUser);
            string refreshToken = this._jwtUtils.RefreshTokenGenerate();

            while(MemoryRefreshTokenTable.Any(rt => rt.RefreshToken == refreshToken))
                refreshToken = this._jwtUtils.RefreshTokenGenerate();

            var alreadyRefreshTokens = MemoryRefreshTokenTable.Where(rt => rt.IdUser == findUser.IdUser && rt.ActiveIs);

            foreach(var alreadyRefreshToken in alreadyRefreshTokens)
            {
                alreadyRefreshToken.RevokeTime = DateTime.Now;
                alreadyRefreshToken.ActiveCheck();
            }

            result.AccessToken = accessToken;
            result.RefreshToken = refreshToken;

            UserRefreshToken userRefreshToken = new()
            {
                IdUser = findUser.IdUser,
                RefreshToken = refreshToken,
                ExpiresTime = DateTime.UtcNow.AddSeconds(1296000),
            };
            userRefreshToken.ActiveCheck();

            MemoryRefreshTokenTable.Add(userRefreshToken);

            this.CookieAccessToken(accessToken);
            this.CookieRefreshToken(refreshToken);

            return result;
        }

        [HttpPut]
        [Authorize]
        public new ActionResult<ApiResultModel> SignOut()
        {
            ApiResultModel result = new()
            {
                Complete = true,
            };

            long? idUser = this._jwtUtils.ClaimDataGet(HttpContext.User);

            if (idUser is not null)
            {
                var alreadyRefreshTokens = MemoryRefreshTokenTable.Where(rt => rt.IdUser == idUser && rt.ActiveIs);

                foreach (var alreadyRefreshToken in alreadyRefreshTokens)
                {
                    alreadyRefreshToken.RevokeTime = DateTime.Now;
                    alreadyRefreshToken.ActiveCheck();
                }


                this.CookieAccessToken("");
                this.CookieRefreshToken("");
            } 

            return result;
        }

        [HttpPut]
        public ActionResult<SignInModel> RefreshToken()
        {
            SignInModel result = new()
            {
                Complete = true,
            };

            string? rt = Request.Cookies[RefreshTokenName];

            string newAccessToken = string.Empty;
            string newRefreshToken = string.Empty;

            if (!string.IsNullOrEmpty(rt))
            {
                var alreadyRefreshToken = MemoryRefreshTokenTable.Where(mrt => mrt.RefreshToken == rt).FirstOrDefault();

                if (alreadyRefreshToken is not null &&
                    alreadyRefreshToken.ActiveCheck().ActiveIs)
                {
                    User? findUser = MemoryUserTable.Where(u => u.IdUser == alreadyRefreshToken.IdUser).FirstOrDefault();

                    if (findUser is not null)
                    {
                        newRefreshToken = rt;
                        newAccessToken = _jwtUtils.AccessTokenGenerate(findUser.IdUser);

                        result.AccessToken = newAccessToken;
                        result.RefreshToken = newRefreshToken;
                    }
                    else
                    {
                        result.Complete = false;
                        Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                }
                else
                {
                    result.Complete = false;
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }

            this.CookieAccessToken(newAccessToken);
            this.CookieRefreshToken(newRefreshToken);

            return result;
        }
    }
}
