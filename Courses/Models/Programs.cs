using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Courses.Models
{
    public class Programs
    {

        public int Id { get; set; }
        [Display(Name = "Course title")]
        public string CourseTitle { get; set; }
        [Display(Name = "Course content")]
        public string CourseContent { get; set; }
        [Display(Name = "Course date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime CourseDate { get; set; }
        [Display(Name = "Course duration")]
        public string CourseDuration { get; set; }
        [Display(Name = "Course image")]
        public string CourseImage { get; set; }
        public int FieldId { get; set; }
        public virtual Fields Field { get; set; }
    }
}