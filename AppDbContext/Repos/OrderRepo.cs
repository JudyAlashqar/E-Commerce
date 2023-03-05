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
    public class OrderRepo : BaseRepo<AppDbContext.Models.Order>, IOrderRepo
    {
        public OrderRepo(ProjectContext db) : base(db)
        {


        }
        public Order getWithDetails(int id)
        {
            return _db.Order.Include(o => o.ProductOrder).Include(o => o.Shipping).Include(o => o.Customer).Where(o => o.Id == id).FirstOrDefault();
        }

        public IEnumerable<Order> getUserOrders(int id)
        {
            return _db.Order.Include(o => o.ProductOrder).Include(o => o.Shipping).Include(o => o.Customer).Where(o => o.CustomerId == id);
        }

        public IEnumerable<Order> getAllWithUsers()
        {
            return _db.Order.Include(o => o.Customer);
        }

        public override void Delete(int id)
        {
            var items = _db.ProductOrder.Where(po => po.OrderId == id);
            foreach(var item in items)
            {
                _db.ProductOrder.Remove(item);
            }
            var shipping = _db.Shipping.Where(s => s.OrderId == id).FirstOrDefault();
            _db.Shipping.Remove(shipping);
            base.Delete(id);
        }
    }
}
