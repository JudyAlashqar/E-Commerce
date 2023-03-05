using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class User : IdentityUser<int>
    {
        public User() 
        {
            Order = new HashSet<Order>();
            QuestionsManager = new HashSet<Questions>();
            QuestionsUser = new HashSet<Questions>();
            Reviews = new HashSet<Reviews>();
            UserHasCoupon = new HashSet<UserHasCoupon>();
            WishList = new HashSet<WishList>();
            CreatedAt = DateTime.Today;
            UpdatedAt = DateTime.Today;
        }

        public override int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gendre { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Questions> QuestionsManager { get; set; }
        public virtual ICollection<Questions> QuestionsUser { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<UserHasCoupon> UserHasCoupon { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
