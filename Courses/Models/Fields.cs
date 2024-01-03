using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class Fields
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Field")]
        public string FieldName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string FieldDescription { get; set; }
        public virtual ICollection<Programs> Course { get; set; }

    }
}