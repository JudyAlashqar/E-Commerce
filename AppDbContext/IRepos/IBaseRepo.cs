using System;
using System.Collections.Generic;
using System.Text;

namespace AppDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        public IEnumerable<T> getAll();
        public T getById(int id);
        public void Add(T obj);
        public void Edit(T obj);
        public void Delete(int id);
    }
}
