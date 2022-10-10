using System.Security.Claims;

namespace JWT.Utils
{
    public interface IJwtUtils
    {
        /// <summary>
        /// 엑세스 토큰 생성
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string AccessTokenGenerate(int idUser);

        /// <summary>
        /// 엑세스 토큰 확인.
        /// </summary>
        /// <remarks>미들웨어에서도 호출해서 사용한다.</remarks>
        /// <param name="token"></param>
        /// <returns>찾아낸 idUser</returns>
        public int? AccessTokenValidate(string token);

        /// <summary>
        /// 리플레시 토큰 생성.
        /// </summary>
        /// <remarks>중복검사는 하지 않으므로 필요하다면 호출한쪽에서 중복검사를 해야 한다.</remarks>
        /// <returns></returns>
        public string RefreshTokenGenerate();

        /// <summary>
        /// HttpContext.User의 클레임을 검색하여 유저 고유정보를 받는다.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public long? ClaimDataGet(ClaimsPrincipal claimsPrincipal);
    }
}
