using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        List<Customer> ICustomerRepository.GetAll()
        {
            return _context.Customers.Include(cus => cus.Country).ToList();
        }
    }
}
