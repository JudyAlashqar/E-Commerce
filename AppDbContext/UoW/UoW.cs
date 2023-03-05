using AppDbContext.Models;
using AppDbContext.Reops;

namespace AppDbContext.UoW
{
    public class UoW : IUoW
    {
        protected readonly ProjectContext _db;
        public ProductRepo productRepo { get; set; }
        public AttributeRepo attributeRepo { get; set; }
        public RoleRepo roleRepo { get; set; }
        public UserRepo userRepo { get; set; }
        public CategoryRepo categoryRepo { get; set; }
        public ProductAttributeValueRepo productAttributeValueRepo { get; set; }
        public CategoryAttributeRepo categoryAttributeRepo { get; set; }
        public CartItemRepo cartItemRepo { get; set; }
        public ShippingRepo shippingRepo { get; set; }
        public OrderRepo orderRepo { get; set; }
        public UoW (ProjectContext db)
        {
            _db = db;
            productRepo = new ProductRepo(_db);
            categoryRepo = new CategoryRepo(_db);
            attributeRepo = new AttributeRepo(_db);
            productAttributeValueRepo = new ProductAttributeValueRepo(_db);
            categoryAttributeRepo = new CategoryAttributeRepo(_db);
            roleRepo = new RoleRepo(_db);
            userRepo = new UserRepo(_db);
            cartItemRepo = new CartItemRepo(_db);
            orderRepo = new OrderRepo(_db);
            shippingRepo = new ShippingRepo(_db);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void Rollback()
        {
            _db.Dispose();
        }
    }
}
