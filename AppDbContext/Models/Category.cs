using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            CategoryAttribute = new HashSet<CategoryAttribute>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CategoryAttribute> CategoryAttribute { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
