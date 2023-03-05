using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDbContext.IRepos;
using AppDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext.Repos
{
    public class BaseRepo <T> : IBaseRepo<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected ProjectContext _db;
        public BaseRepo (ProjectContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public IEnumerable<T> getAll()
        {
            return _dbSet.ToList();
        }
        public T getById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T obj)
        {
            _dbSet.Add(obj);
        }
        public void Edit(T obj)
        {
            _dbSet.Update(obj);
        }
        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }
    }
}
