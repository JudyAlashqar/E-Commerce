using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Reops
{
    public class CategoryAttributeRepo : BaseRepo<CategoryAttribute>, ICategoryAttributeRepo
    {
        public CategoryAttributeRepo(ProjectContext db) : base(db)
        {

        }
        
    }
}
