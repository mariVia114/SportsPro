using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models.ManageViewModels
{
    public class TechnicianIncidentViewModel
    {
        public List<SelectListItem> Technicians { get; set; }
        [Required(ErrorMessage = "Technician is Required")]
        public int TechnicianId { get; set; }
    }
}
