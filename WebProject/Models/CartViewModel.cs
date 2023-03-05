using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            products = new List<Product>();
            cartItems = new List<CartItem>();
        }
        public IEnumerable<Product> products;
        public IEnumerable<CartItem> cartItems;

    }
}
