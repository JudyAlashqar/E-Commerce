using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class AttributeViewModel
    {
        public AttributeViewModel()
        {
        }

        [Required(ErrorMessage ="Attribute Name must not be empty")]
        [Display(Name = "Attribute Name")]
        public string Name { get; set; }

    }
}
