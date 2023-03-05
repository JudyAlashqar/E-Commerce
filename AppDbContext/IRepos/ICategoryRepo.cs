using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;
using AppDbContext.IRepos;

namespace AppDbContext.IRepos
{
    public interface ICategoryRepo : IBaseRepo<Category>
    {
        public IEnumerable<AppDbContext.Models.Attribute> getCategoryAttributes(int id);
        public IEnumerable<AppDbContext.Models.Attribute> getNonCategoryAttributes(int id);
        public IEnumerable<Product> getCategoryProducts(int id);
        public int checkUnique(string Name);
        public int checkDelete(int id);
        public Category getWithAttributeById(int id);
        public void deleteCategoryAttributes(int Id);
    }
    
}
