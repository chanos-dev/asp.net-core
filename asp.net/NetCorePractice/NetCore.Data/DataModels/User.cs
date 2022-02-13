using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore.Data.DataModels
{
    [Table("user")]
    public class User
    {
        [Key, StringLength(12), Column(TypeName ="varchar(12)")]
        public string UserId { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar(100)")]
        public string UserEmail { get; set; }

        [Required, StringLength(12), Column(TypeName = "varchar(12)")]
        public string UserPw { get; set; }

        [Required]
        public bool IsMembershipWithdrawn { get; set; }

        [Required]
        public DateTime JoinedUTCDate { get; set; }

        [ForeignKey("UserId")]
        public ICollection<UserRolesByUser> UserRolesByUsers { get; set; }
    }
}
