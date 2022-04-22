using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models.ManageViewModels
{
    public class CustomerProductViewModel
    {
        public List<SelectListItem> Customers { get; set; }
        [Required(ErrorMessage = "Customer is Required")]
        public int CustomerId { get; set; }
    }
}
