using System.ComponentModel.DataAnnotations;

namespace SportsPro.Dtos
{
    public class IncidentDto
    {
        public int IncidentId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must have atleast {2} and less than {1}  characters")]
        public string? Title { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = "Description must have atleast {2} and less than {1}  characters")]
        public string? Description { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }

        [Display(Name = "Technician")]
        public int? TechnicianId { get; set; }
        public TechnicianDto? Technician { get; set; }

        [Display(Name = "Date Opened")]
        public DateTime DateOpened { get; set; }

        [Display(Name = "Date Closed")]
        public DateTime? DateClosed { get; set; }
    }
}
