using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDbContext.Models
{
    public partial class CartItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int? CartId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
