using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.Dtos;

namespace SportsPro.Models.IncidentModels
{
    public class IncidentViewModel
    {
        public IncidentViewModel()
        {
            CustomerList = new List<SelectListItem>();
            ProductList = new List<SelectListItem>();
            TechnicianList = new List<SelectListItem>() { new SelectListItem() { Value = null, Text = "Select a technician" } };
        }
        public List<IncidentDto>? IncidentList { get; set; }
        public IncidentDto? Incident { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> TechnicianList { get; set; }

    }
}
