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
    public class CouponRepo : BaseRepo<Coupon>, ICouponRepo
    {
        public CouponRepo(ProjectContext db) : base(db)
        {

        }
        public IEnumerable<Category> getCategoriesWithName(string name)
        {
            return _db.Category.Where(s => s.Name == name);
        }

        public void deleteCategoryAttributes(int Id)
        {
            var entites = _db.CategoryAttribute.Where(e => e.CategoryId == Id);
            foreach (var entity in entites)
            {
                _db.CategoryAttribute.Remove(entity);
            }
        }

        public IEnumerable<Product> getCategoryProducts(int id)
        {
            return _db.Product.Where(p => p.CategoryId == id);
        }
        public override void Delete(int id)
        {
            var products = _db.Product.Where(p => p.CategoryId == id).ToList();
            foreach(var product in products)
            {
                _db.ProductAttributeValue.RemoveRange(_db.ProductAttributeValue.Where(p => p.ProductId == product.Id));
                _db.Product.Remove(product);
            }
            var catAtt = _db.CategoryAttribute.Where(c => c.CategoryId == id);
            foreach (var att in catAtt)
            {
                _db.CategoryAttribute.Remove(att);
            }
            base.Delete(id);
        }

        public IEnumerable<AppDbContext.Models.Attribute> getCategoryAttributes(int id)
        {
            return _db.CategoryAttribute.Include(ca => ca.Attribute).Where(c => c.CategoryId == id).Select(ca => ca.Attribute);
        }
        public IEnumerable<AppDbContext.Models.Attribute> getNonCategoryAttributes(int id)
        {
            var a = _db.CategoryAttribute.Include(ca => ca.Attribute).Where(Ca => Ca.CategoryId == id).Select(c => c.Attribute);
            var b = from Attribute in _db.Attribute select Attribute;
            return b.Except(a);
            //return _db.Attribute.Include(a => a.CategoryAttribute).Where(c => !c.CategoryAttribute.Contains(id)).Select(ca => ca.AttributeId);
        }
        public int checkUnique(string Name)
        {
            int id = _db.Category.Where(p => p.Name == Name).Select(p => p.Id).FirstOrDefault();
            return id;
        }
        public int checkDelete(int id)
        {
            //return _db.Product.Include(p => p.ProductAttributeValue).Where(pav => pav.Id == id).Select(pav => pav.ProductAttributeValue.Count()).FirstOrDefault();
            return _db.CategoryAttribute.Where(ca => ca.CategoryId == id).Select(pav => pav.Id).Count()
                + _db.Product.Where(p => p.CategoryId == id).Select(p => p.Id).Count();
        }
        public Category getWithAttributeById(int id)
        {
            return _db.Category.Include(c => c.CategoryAttribute).Where(p => p.Id == id).FirstOrDefault();
        }

    }
}
