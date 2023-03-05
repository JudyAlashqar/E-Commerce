using AppDbContext.IRepos;
using AppDbContext.Models;
using AppDbContext.Repos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.Reops
{
    public class RoleRepo : BaseRepo<AppDbContext.Models.AspNetRoles>, IRoleRepo
    {
        public RoleRepo(ProjectContext db) : base(db)
        {


        }
        public int checkUnique(string Name)
        {
            int id = _db.Role.Where(p => p.Name == Name).Select(p => p.Id).FirstOrDefault();
            return id;
        }
        public int checkDelete(int id)
        {
            return _db.UserRoles.Where(ca => ca.RoleId == id).Select(pav => pav.RoleId).Count()
                + _db.RoleClaims.Where(p => p.RoleId == id).Select(p => p.Id).Count();
        }
    }
}
