using SportsPro.Data;

namespace SportsPro.Infrastructure.Interfaces
{
    public interface IRegistrationRepository : IGenericRepository<Registration>
    {
        public List<Registration> GetAll();
        public List<Registration> GetAllByCustomer(int customerId);
        public Registration Get(int customerId, int productId);
    }
}
