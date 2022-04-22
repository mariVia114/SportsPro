using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context) { }
    }
}
