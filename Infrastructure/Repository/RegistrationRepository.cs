using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context) { }

        public Registration Get(int customerId, int productId)
        {
            return _context.Registrations.SingleOrDefault(r => r.ProductId == productId && r.CustomerId == customerId);
        }

        public List<Registration> GetAllByCustomer(int customerId)
        {
            return _context.Registrations.Include(r => r.Product).Where(i => i.CustomerId == customerId).ToList();
        }

        List<Registration> IRegistrationRepository.GetAll()
        {
            return _context.Registrations.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Customer).ToList(); 
        }
    }
}
