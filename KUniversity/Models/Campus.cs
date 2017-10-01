using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Campus
    {
        public Campus()
        {
            Department = new HashSet<Department>();
        }

        public int Id { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "校区名称")]
        public string Name { get; set; }

        public virtual ICollection<Department> Department { get; set; }

        public string Code => $"{(char)(('A') + Id)}";
    }
}
