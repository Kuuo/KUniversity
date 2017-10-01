using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Enrollment
    {
        public int Id { get; set; }
        public int CourseAssignmentId { get; set; }

        [Display(Name = "成绩")]
        public decimal Grade { get; set; }
        public int StudentId { get; set; }

        public virtual Courseassignment CourseAssignment { get; set; }
        public virtual Student Student { get; set; }
    }
}
