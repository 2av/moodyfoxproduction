using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodyfox.Models
{
    public class EnquiryFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Contact { get; set; }
        public string ReferenceVideo { get; set; }
        public string ServiceRequirement { get; set; }
        public string Message { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime EnquiryDate { get; set; }
    }

}