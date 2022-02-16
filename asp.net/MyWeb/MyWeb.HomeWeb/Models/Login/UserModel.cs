using MyWeb.Lib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Models.Login
{
    public class UserModel
    {
        public int UserSeq { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }

        internal string ConvertToPassWord()
        {
            var sha = new HMACSHA512();
            sha.Key = Encoding.UTF8.GetBytes(this.PassWord.Length.ToString());

            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(this.PassWord));
            return Convert.ToBase64String(hash);
        }

        internal UserModel GetLoginUser()
        {
            string sql = @"
select user_seq UserSeq, user_name UserName, email, password from t_user where user_name = @user_name
";

            UserModel user;
            using (var conn = new DBHelper())
            {
                user = conn.QuerySingleOrDefault<UserModel>(sql, new { @user_name = UserName });
            }

            if (user is null)
                throw new Exception("사용자가 존재하지 않습니다");

            if (user.PassWord != ConvertToPassWord())
                throw new Exception("비밀번호가 틀렸습니다");

            return user;
        }

        internal int Register()
        {
            string sql = @"
insert into t_user (user_name, email, password)
values (@user_name, @email, @password)
";
            using (var conn = new DBHelper())
            {
                return conn.Execute(sql, new { @user_name = UserName, @email = Email, @password = ConvertToPassWord() });
            }
        }
    }
}
