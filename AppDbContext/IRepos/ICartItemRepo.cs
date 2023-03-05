using System;
using System.Collections.Generic;
using System.Text;
using AppDbContext.Models;

namespace AppDbContext.IRepos
{
    public interface ICartItemRepo : IBaseRepo<AppDbContext.Models.CartItem>
    {
        public void AddToCart(int cartId, int productId);
        public void RemoveFromCart(int cartId, int productId);

        public void Remove(int cartId, int productId);
        public void EmptyCart(int cartId);
        public void MigrateCart(int userId, int cartId);
        public IEnumerable<CartItem> ViewCart(int cartId);
    }
}
