namespace JWT.Model
{
    public class SignInModel
    {
        /// <summary>
        /// 성공여부
        /// </summary>
        public bool Complete { get; set; } = false;

        /// <summary>
        /// 성공시 검색된 유저의 고유번호
        /// </summary>
        public long idUser { get; set; } = 0;

        /// <summary>
        /// 엑세스 토큰
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// 라플레시 토큰
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
