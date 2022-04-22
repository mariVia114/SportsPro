using SportsPro.Data;
namespace SportsPro.Infrastructure.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        bool ProductExists(int id);
        List<Product> GetAllByCustomer(int customerId);
    }
}
