using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            total = 0.0;
        }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [RegularExpression(@"(^09[0-9]{8}$)", ErrorMessage = "Phone Number must be a Syrian Mobile Phone Number (10 digits and the first two are 09)")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public double total { get; set; }

    }
}
