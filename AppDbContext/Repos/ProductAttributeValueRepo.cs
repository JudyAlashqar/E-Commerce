using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext.Reops
{
    public class ProductAttributeValueRepo : BaseRepo<ProductAttributeValue>, IProductAttributeValue
    {
        public ProductAttributeValueRepo(ProjectContext db) : base(db)
        {

        }
    }
}
