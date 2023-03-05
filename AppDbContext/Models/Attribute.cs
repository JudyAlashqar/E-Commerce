using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Attribute : BaseEntity
    {
        public Attribute()
        {
            CategoryAttribute = new HashSet<CategoryAttribute>();
            ProductAttributeValue = new HashSet<ProductAttributeValue>();
        }

        public int Id { get; set; }
        [Display(Name = "Attribute Name")]
        public string Name { get; set; }

        public virtual ICollection<CategoryAttribute> CategoryAttribute { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
    }
}
