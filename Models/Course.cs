using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SbdS.Models
{
    public class Course

    {
        public int CourseId { get; set; }
        [Display(Name="Kursnavn")]
        public String Name { get; set; }
        [Display(Name = "Dag")]
        public String Day { get; set; }
        [Display(Name = "Instruktør")]
        public string Instructor { get; set; }

        
        public List<UserAtCourse> UserAtCourse { get; set; }




    }
}