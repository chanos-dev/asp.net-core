using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Data.ViewModels
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "아이디를 입력하세요.")]
        [MinLength(4, ErrorMessage = "아이디는 4자 이상 입력하세요.")]
        [Display(Name = "아이디")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        [MinLength(4, ErrorMessage = "비밀번호는 4자 이상 입력하세요.")]
        [Display(Name = "비밀번호")]
        public string UserPW { get; set; }
    }
}
