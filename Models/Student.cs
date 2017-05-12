using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SbdS.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Fornavn")]
        public string Fname { get; set; }

        [Display(Name = "Etternavn")]
        public string Lname { get; set; }

        [Display(Name = "Adresse")]
        public string Adr { get; set; }
        [Display(Name = "Brukernavn")]
        public string Username { get; set; }
        [Display(Name = "Alder")]
        public int Age { get; set; }
        [Display(Name = "Foresattes navn")]
        public string ParentName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        


        [DefaultValue(false)]
        public bool IsAdmin { get; set; }


        public List<UserAtCourse> UserAtCourse { get; set; }

    }
}