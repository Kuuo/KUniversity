using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
            InstructorNavigation = new HashSet<Instructor>();
            Major = new HashSet<Major>();
        }

        public int Id { get; set; }

        [Display(Name = "所在校区")]
        public int CampusId { get; set; }

        [Display(Name = "学院管理员")]
        public int? InstructorId { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "学院名称")]
        public string Name { get; set; }

        [Display(Name = "建立时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<Instructor> InstructorNavigation { get; set; }
        public virtual ICollection<Major> Major { get; set; }

        [Display(Name = "所在校区")]
        public virtual Campus Campus { get; set; }

        [Display(Name = "管理员")]
        public virtual Instructor Instructor { get; set; }
    }
}
