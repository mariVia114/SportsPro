using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;

namespace SportsPro.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(
            ICountryRepository country,
            ICustomerRepository customer,
            IIncidentRepository incident,
            IProductRepository product,
            IRoleRepository role,
            ITechnicianRepository technician,
            IRegistrationRepository registration,
            ApplicationDbContext context)
        {
            Country = country;
            Customer = customer;
            Incident = incident;
            Product = product;
            Role = role;
            Technician = technician;
            Registration = registration;
            _context = context;
        }
        public ICountryRepository Country
        {
            get;
            private set;
        }
        public ICustomerRepository Customer
        {
            get;
            private set;
        }
        public IIncidentRepository Incident
        {
            get;
            private set;
        }
        public IProductRepository Product
        {
            get;
            private set;
        }
        public IRoleRepository Role
        {
            get;
            private set;
        }
        public ITechnicianRepository Technician
        {
            get;
            private set;
        }
        public IRegistrationRepository Registration
        {
            get;
            private set;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
