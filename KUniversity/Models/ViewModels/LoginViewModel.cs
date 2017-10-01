using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KUniversity.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "此项不可为空")]
        public int AccountID { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "此项不可为空")]
        [MinLength(6, ErrorMessage = "密码不少于6位")]
        [MaxLength(20, ErrorMessage = "密码不多于20位")]
        public string Password { get; set; }
    }
}
