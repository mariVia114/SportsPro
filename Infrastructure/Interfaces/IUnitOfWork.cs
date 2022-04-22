namespace SportsPro.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICountryRepository Country
        {
            get;
        }
        ICustomerRepository Customer
        {
            get;
        }
        IIncidentRepository Incident
        {
            get;
        }
        IProductRepository Product
        {
            get;
        }
        IRoleRepository Role
        {
            get;
        }
        ITechnicianRepository Technician
        {
            get;
        }
        IRegistrationRepository Registration
        {
            get;
        }
        int Save();
    }
}
