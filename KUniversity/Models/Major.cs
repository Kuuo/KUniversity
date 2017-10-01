using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Major
    {
        public Major()
        {
            Eclass = new HashSet<Eclass>();
        }

        public int Id { get; set; }

        [Display(Name = "所在学院")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "专业名称")]
        public string Title { get; set; }

        public virtual ICollection<Eclass> Eclass { get; set; }
        public virtual Department Department { get; set; }
    }
}
