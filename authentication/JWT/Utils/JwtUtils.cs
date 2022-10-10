using JWT.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWT.Utils
{
    public class JwtUtils : IJwtUtils
    {
        private readonly JwtAuthSettingModel _jwtAuthSettingModel;

        public JwtUtils(IOptions<JwtAuthSettingModel> appSettings)
        {
            this._jwtAuthSettingModel = appSettings.Value;

            if (this._jwtAuthSettingModel is null)
                throw new ArgumentNullException(nameof(appSettings));

            if (string.IsNullOrEmpty(this._jwtAuthSettingModel.Secret))
                this._jwtAuthSettingModel.Secret = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public string AccessTokenGenerate(int idUser)
        { 
            byte[] key = Encoding.ASCII.GetBytes(this._jwtAuthSettingModel.Secret!);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("idUser", idUser.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? AccessTokenValidate(string token)
        {
            if (token is null)
                return null;

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(this._jwtAuthSettingModel.Secret!);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken)
                    return null;

                if (!int.TryParse(jwtToken.Claims.First(x => x.Type == "idUser").Value, out int accountId))
                    return null;

                return accountId;
            }
            catch
            {
                return null;
            }
        }

        public long? ClaimDataGet(ClaimsPrincipal claimsPrincipal)
        {
            foreach(var item in claimsPrincipal.Claims)
            {
                if (item.Type == "idUser")
                {
                    if (!long.TryParse(item.Value, out long accountId))
                        return null;

                    return accountId;
                }
            }

            return null;
        }

        public string RefreshTokenGenerate() => Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}
