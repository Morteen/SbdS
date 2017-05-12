using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SbdS.Models
{
    public class UserAtCourse
    {
        [Key]
        public int AtCourseId { get; set; }
     
       
        public bool IsPayed { get; set; }


        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
       

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}