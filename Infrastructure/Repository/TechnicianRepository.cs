using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;
namespace SportsPro.Infrastructure.Repository
{
    public class TechnicianRepository : GenericRepository<Technician>, ITechnicianRepository
    {
        public TechnicianRepository(ApplicationDbContext context) : base(context) { 
        }

        public Technician FindByEmail(string email)
        {
            email = email.ToLower();
            return _context.Technicians.FirstOrDefault(t => t.Email!.ToLower().Equals(email));
        }

        public bool TechnicianExists(int id)
        {
            return _context.Technicians.Any(t => t.TechnicianId == id);
        }
    }
}
