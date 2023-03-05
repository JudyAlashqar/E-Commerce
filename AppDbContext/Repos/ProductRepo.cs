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
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(ProjectContext db) : base(db)
        {

        }
        public override void Delete(int id)
        {
            var attributes = _db.ProductAttributeValue.Where(c => c.ProductId == id).ToList();
            foreach (var attribute in attributes)
            {
                _db.ProductAttributeValue.Remove(attribute);
            }
            base.Delete(id);
        }
        public IEnumerable<Product> getWithCategories()
        {
            return _db.Product.Include(p => p.Category);
        }
        public Product getWithCategory(int id)
        {
            return _db.Product.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault();
        }
        public int checkUnique(string sku)
        {
            int id = _db.Product.Where(p => p.Sku == sku).Select(p => p.Id).FirstOrDefault();
            return id;
        }
        public int checkDelete(int id)
        {
            return _db.ProductOrder.Where(pav => pav.ProductId == id).Select(pav => pav.Id).Count()
                + _db.CartItem.Where(pav => pav.ProductId == id).Select(p => p.Id).Count();
        }
        public IEnumerable<ProductAttributeValue> getProductWithAttributes(int productId)
        {
            return _db.ProductAttributeValue.Include(pav => pav.Attribute).Where(pav => pav.ProductId == productId);
        }
        public void DeleteOnProductId(int productId)
        {
            var attributes = _db.ProductAttributeValue.Where(pav => pav.ProductId == productId);
            foreach (var attribute in attributes)
            {
                _db.ProductAttributeValue.Remove(attribute);
            }
        }
    }
}
