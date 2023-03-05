using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        [Required(ErrorMessage ="Role Name must not be empty")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
