using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Reops
{
    public class AttributeRepo : BaseRepo<AppDbContext.Models.Attribute>, IAttributeRepo
    {
        public AttributeRepo(ProjectContext db) : base(db)
        {


        }
        public int checkUnique(string Name)
        {
            int id = _db.Attribute.Where(p => p.Name == Name).Select(p => p.Id).FirstOrDefault();
            return id;
        }
        public int checkDelete(int id)
        {
            return _db.ProductAttributeValue.Where(pav => pav.AttributeId == id).Select(pav => pav.Id).Count()
                + _db.CategoryAttribute.Where(pav => pav.AttributeId== id).Select(pav => pav.Id).Count();
        }
    }
}
