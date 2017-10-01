using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Course
    {
        public Course()
        {
            Courseassignment = new HashSet<Courseassignment>();
        }

        public int Id { get; set; }
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "课程名称")]
        public string Title { get; set; }

        public virtual ICollection<Courseassignment> Courseassignment { get; set; }

        [Display(Name = "所在学院")]
        public virtual Department Department { get; set; }
    }
}
