using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class values
    {
        public values()
        {
        }
        public string Value { get; set; }
        public string Unit { get; set; }
    }
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Categories = null;
            CategoryAttributes = null;
            NonCategoryAttributes = null;
            AddedAttributes = null;
            ProductAttributeValues = null;
            SelectedAttributes = new List<int>();
            AddedProductAttributeValue = new List<values>();
        }
        [Required(ErrorMessage ="Product Name must not be empty")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "SKU must not be empty")]
        [Display(Name = "Product SKU")]
        public string Sku { get; set; }
       
        [Required(ErrorMessage = "Price must not be empty")]
        [Display(Name = "Unit Price")]
        public double Price { get; set; }

        [Display(Name = "Product Image")]
        public string Image1 { get; set; }

        public string Description { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company Name must not be empty")]
        public string CompanyName { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<AppDbContext.Models.Attribute> NonCategoryAttributes { get; set; }
        public IEnumerable<AppDbContext.Models.Attribute> CategoryAttributes { get; set; }
        public IEnumerable<AppDbContext.Models.Attribute> AddedAttributes { get; set; }
        public ICollection<int> SelectedAttributes { get; set; }
        public ICollection<values> AddedProductAttributeValue { get; set; }
        public IEnumerable<ProductAttributeValue> ProductAttributeValues { get; set; }

    }
}
