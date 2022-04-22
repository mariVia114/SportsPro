using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class IncidentRepository : GenericRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(ApplicationDbContext context) : base(context) { }

        public Incident Get(int id)
        {
            return _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician).SingleOrDefault(i => i.IncidentId == id);
        }

        public List<Incident> GetAllByTechnician(int id)
        {
            return _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Where(i => i.TechnicianId == id).ToList();
        }

        public bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentId == id);
        }

        List<Incident> IIncidentRepository.GetAll()
        {
            return _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician).ToList();
        }

    }
}
