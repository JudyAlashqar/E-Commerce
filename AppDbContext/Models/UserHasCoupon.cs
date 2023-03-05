using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class UserHasCoupon : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual User User { get; set; }
    }
}
