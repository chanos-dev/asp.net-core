using NetCore.Data.DataModels;
using NetCore.Data.ViewModels;
using NetCore.Services.Data;
using NetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Services.Svcs
{
    public class UserService : IUser
    {
        private CodeFirstDbContext DbContext { get; set; }

        public UserService(CodeFirstDbContext context)
        {
            this.DbContext = context;
        }

        private IEnumerable<User> GetUserInfos() => DbContext.User.ToArray();

        private bool CheckUserInfo(string userId, string userPw)
        {
            return GetUserInfos().Any(user => user.UserId == userId && user.UserPw == userPw);
        }

        public bool MatchUserInfo(LoginInfo loginInfo)
        {
            return CheckUserInfo(loginInfo.UserID, loginInfo.UserPW);
        }
    }
}
