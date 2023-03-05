using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class ProductOrder : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
