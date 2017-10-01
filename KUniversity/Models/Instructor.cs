using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courseassignment = new HashSet<Courseassignment>();
            Department = new HashSet<Department>();
            Eclass = new HashSet<Eclass>();
        }

        [Display(Name = "教工号")]
        public int Id { get; set; }

        [Display(Name = "学历")]
        public string Academic { get; set; }
        public int? DepartmentId { get; set; }

        [Display(Name = "邮件")]
        public string Email { get; set; }

        [Display(Name = "性别")]
        public int? Gender { get; set; }

        [Display(Name = "入校时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "姓名")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "民族")]
        public string Nation { get; set; }

        [Display(Name = "办公室")]
        public string OfficeLocation { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [MinLength(6, ErrorMessage = "密码至少6位")]
        [MaxLength(20, ErrorMessage = "密码至多20位")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Courseassignment> Courseassignment { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Eclass> Eclass { get; set; }
        public virtual Department DepartmentNavigation { get; set; }
    }
}
