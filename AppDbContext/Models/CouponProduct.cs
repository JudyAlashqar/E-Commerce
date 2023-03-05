using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class CouponProduct : BaseEntity
    {
        public int CouponId { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }
        public double Discount { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Product Product { get; set; }
    }
}
