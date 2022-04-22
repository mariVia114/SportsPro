using AutoMapper;
using SportsPro.Data;
using SportsPro.Dtos;

namespace SportsPro.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add the Mappings Here.
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Incident, IncidentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Registration, RegistrationDto>().ReverseMap();
            CreateMap<Technician, TechnicianDto>().ReverseMap();
        }
    }
}
