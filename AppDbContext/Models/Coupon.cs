using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Coupon : BaseEntity
    {
        public Coupon()
        {
            CouponProduct = new HashSet<CouponProduct>();
            UserHasCoupon = new HashSet<UserHasCoupon>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<CouponProduct> CouponProduct { get; set; }
        public virtual ICollection<UserHasCoupon> UserHasCoupon { get; set; }
    }
}
