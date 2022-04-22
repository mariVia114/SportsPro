using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public List<Product> GetAllByCustomer(int customerId)
        {
            return _context.Products.Where(p => p.Registrations.Any(r => r.ProductId == p.ProductId && r.CustomerId == customerId) == false).ToList();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
