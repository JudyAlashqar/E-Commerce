using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface IAttributeRepo : IBaseRepo<AppDbContext.Models.Attribute>
    {
        public int checkUnique(string Name);
        public int checkDelete(int id);
    }
}
