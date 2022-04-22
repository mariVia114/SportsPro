using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;
namespace SportsPro.Infrastructure.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) { }
    }
}
