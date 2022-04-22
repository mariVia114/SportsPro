using System.ComponentModel.DataAnnotations;

namespace SportsPro.Data
{
    public class Incident
    {
        public int IncidentId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must have atleast {2} and less than {1}  characters")]
        public string Title { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = "Description must have atleast {2} and less than {1}  characters")]
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int? TechnicianId { get; set; }
        public Technician Technician { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
    }
}
