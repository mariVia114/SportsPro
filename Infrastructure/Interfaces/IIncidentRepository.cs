using SportsPro.Data;
namespace SportsPro.Infrastructure.Interfaces
{
    public interface IIncidentRepository : IGenericRepository<Incident>
    {
        List<Incident> GetAll();
        List<Incident> GetAllByTechnician(int id);
        Incident Get(int id);
        bool IncidentExists(int id);
    }
}
