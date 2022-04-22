using SportsPro.Data;

namespace SportsPro.Infrastructure.Interfaces
{
    public interface ITechnicianRepository : IGenericRepository<Technician>
    {
        bool TechnicianExists(int id);
        Technician FindByEmail(string email);
    }
}
