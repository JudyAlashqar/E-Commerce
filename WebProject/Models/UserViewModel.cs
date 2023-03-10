using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            rolesIds = new List<int>();
            roles = new List<AspNetRoles>();
            rolesNames = new List<string>();
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "User First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "User Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "User Address")]
        public string Address { get; set; }

        [Display(Name = "User Gender")]
        public string Gendre { get; set; }

        [Required]
        [RegularExpression(@"(^09[0-9]{8}$)", ErrorMessage = "Phone Number must be a Syrian Mobile Phone Number (10 digits and the first two are 09)")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Personal Photo")]
        public string Image { get; set; }
        public List<int> rolesIds { get; set; }
        public List<AspNetRoles> roles { get; set; }
        public List<string> rolesNames { get; set; }
    }
}
