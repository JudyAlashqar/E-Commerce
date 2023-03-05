using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Reops
{
    public class ShippingRepo : BaseRepo<AppDbContext.Models.Shipping>, IShippingRepo
    {
        public ShippingRepo(ProjectContext db) : base(db)
        {


        }
       
    }
}
