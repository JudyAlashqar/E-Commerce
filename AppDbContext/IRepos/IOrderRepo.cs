using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface IOrderRepo : IBaseRepo<AppDbContext.Models.Order>
    {
        public Order getWithDetails(int id);
        public IEnumerable<Order> getUserOrders(int id);
        public IEnumerable<Order> getAllWithUsers();
    }
}
