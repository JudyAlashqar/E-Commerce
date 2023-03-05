using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            SelectedAttributes = new List<int> ();
            AddedAttributes = new List<AppDbContext.Models.Attribute>();
            Attributes = null;
            CategoryAttributes = null;
            cat_products = null;
        }

        [Required(ErrorMessage ="Category Name must not be empty")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable <Product> cat_products { get; set; }
        public IEnumerable <AppDbContext.Models.Attribute> Attributes { get; set; }

        public List<int> SelectedAttributes { get; set; }

        public ICollection<AppDbContext.Models.Attribute> AddedAttributes { get; set; }


        public IEnumerable<AppDbContext.Models.Attribute> CategoryAttributes { get; set; } // referene to details of category


    }
}
