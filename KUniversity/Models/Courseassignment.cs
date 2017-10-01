using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KUniversity.Models
{
    public partial class Courseassignment
    {
        public Courseassignment()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "教室1")]
        public string ClassRoom1 { get; set; }

        [Display(Name = "教室2")]
        public string ClassRoom2 { get; set; }

        [Display(Name = "教室3")]
        public string ClassRoom3 { get; set; }

        [Required(ErrorMessage = "该项不可为空")]
        [Display(Name = "上课时间1")]
        public string ClassTime1 { get; set; }

        [Display(Name = "上课时间2")]
        public string ClassTime2 { get; set; }

        [Display(Name = "上课时间3")]
        public string ClassTime3 { get; set; }
        public int CourseId { get; set; }

        [Display(Name = "结束周次")]
        public int EndWeek { get; set; }
        public int InstructorId { get; set; }

        [Display(Name = "班次")]
        public int Order { get; set; }

        [Display(Name = "学期")]
        public int Semester { get; set; }

        [Display(Name = "开始周次")]
        public int StartWeek { get; set; }

        [Display(Name = "学年")]
        public int Year { get; set; }

        public virtual ICollection<Enrollment> Enrollment { get; set; }

        [Display(Name = "课程名")]
        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }


        public bool ClassTimeInDayOfWeek(DayOfWeek dayOfWeek)
        {
            if (Time1DayOfWeek.HasValue && Time1DayOfWeek == dayOfWeek) return true;
            if (Time2DayOfWeek.HasValue && Time2DayOfWeek == dayOfWeek) return true;
            if (Time3DayOfWeek.HasValue && Time3DayOfWeek == dayOfWeek) return true;
            return false;
        }


        public DayOfWeek? Time1DayOfWeek {
            get {
                if (string.IsNullOrEmpty(ClassTime1))
                    return null;
                return (DayOfWeek)int.Parse(ClassTime1[0].ToString());
            }
        }

        public DayOfWeek? Time2DayOfWeek {
            get {
                if (string.IsNullOrEmpty(ClassTime2))
                    return null;
                return (DayOfWeek)int.Parse(ClassTime2[0].ToString());
            }
        }

        public DayOfWeek? Time3DayOfWeek {
            get {
                if (string.IsNullOrEmpty(ClassTime3))
                    return null;
                return (DayOfWeek)int.Parse(ClassTime3[0].ToString());
            }
        }

        public CourseTimeLocation ClassTimeLocation(DayOfWeek dayOfWeek)
        {
            if (Time1DayOfWeek.HasValue && Time1DayOfWeek == dayOfWeek)
                return new CourseTimeLocation { Time = ClassTime1.Substring(2, ClassTime1.Length - 2), Location = ClassRoom1 };

            if (Time2DayOfWeek.HasValue && Time2DayOfWeek == dayOfWeek)
                return new CourseTimeLocation { Time = ClassTime2.Substring(2, ClassTime1.Length - 2), Location = ClassRoom2 };

            if (Time3DayOfWeek.HasValue && Time3DayOfWeek == dayOfWeek)
                return new CourseTimeLocation { Time = ClassTime3.Substring(2, ClassTime1.Length - 2), Location = ClassRoom3 };

            return null;
        }


        public string OrderText => $"{Order:D2}";


        public class CourseTimeLocation
        {
            public string Time { get; set; }
            public string Location { get; set; }
        }
    }
}
