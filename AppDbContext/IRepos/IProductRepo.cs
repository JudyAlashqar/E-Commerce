using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        public IEnumerable<Product> getWithCategories();
        public Product getWithCategory(int id);
        public int checkUnique(string sku);
        public IEnumerable<ProductAttributeValue> getProductWithAttributes(int productId);
        public void DeleteOnProductId(int productId);
    }
}
