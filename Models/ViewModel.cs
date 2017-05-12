using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SbdS.Models
{
    public class ViewModel
    {
        public List<Student> StudentList { get; set; }
        public List<Course> CourseList { get; set; }
        public List<UserAtCourse> UserAtCourseList { get; set; }


    }
}