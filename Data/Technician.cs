using System.ComponentModel.DataAnnotations;

namespace SportsPro.Data
{
    public class Technician
    {
        public int TechnicianId { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Name must have atleast {2} and less than {1} characters")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ICollection<Incident> Incidents { get; set; }
    }
}
