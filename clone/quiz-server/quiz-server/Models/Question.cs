using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_server.Models
{
    public class Question
    {
        [Key]
        public int QnId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string QnInWords { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? ImageName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Option1 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Option2 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Option3 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Option4 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Answer { get; set; }        
    }
}
