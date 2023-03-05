using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface IRoleRepo : IBaseRepo<AppDbContext.Models.AspNetRoles>
    {
        public int checkUnique(string Name);
        public int checkDelete(int id);


    }
}
