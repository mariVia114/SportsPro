using SportsPro.Data;

namespace SportsPro.Infrastructure.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer> 
    {
        public List<Customer> GetAll();
        bool CustomerExists(int id);
    }
   
}
