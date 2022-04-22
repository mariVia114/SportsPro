using Microsoft.AspNetCore.Mvc.Rendering;
using SportsPro.Data;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models.RegistrationViewModels
{
    public class RegistrationViewModel
    {
        public IList<Registration> Registrations { get; set; }
        public IList<SelectListItem> Products { get; set; }    
        [Required(ErrorMessage = "Product is Required")]
        
        public int ProductId { get; set; }
    }
}
