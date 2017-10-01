using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUniversity.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        [Display(Name = "学号")]
        public int Id { get; set; }
        public int? EclassId { get; set; }

        [Display(Name = "邮件")]
        public string Email { get; set; }

        [Display(Name = "入校时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "性别")]
        public int? Gender { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "姓名")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "民族")]
        public string Nation { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        [MaxLength(20, ErrorMessage = "密码至多20位")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Enrollment> Enrollment { get; set; }

        [Display(Name = "专业班级")]
        public virtual Eclass Eclass { get; set; }
    }
}
