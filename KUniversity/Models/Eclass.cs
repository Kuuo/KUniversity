using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Eclass
    {
        public Eclass()
        {
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int MajorId { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "班号")]
        public int Order { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "学年")]
        public int StartYear { get; set; }

        public virtual ICollection<Student> Student { get; set; }

        [Display(Name = "班主任")]
        public virtual Instructor Instructor { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "专业")]
        public virtual Major Major { get; set; }
    }
}
