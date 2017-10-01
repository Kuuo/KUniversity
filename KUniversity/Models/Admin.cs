using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "该项不可为空"), MinLength(6), MaxLength(24)]
        public string Password { get; set; }

        [Required(ErrorMessage = "该项不可为空"), MaxLength(20)]
        [Display(Name = "用户名")]
        public string Username { get; set; }
    }
}
