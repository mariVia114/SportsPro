using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models.IncidentModels
{
    public class UpdateIncidentViewModel
    {
        [Required]
        public int IncidentId { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DateClosed { get; set; }


    }
}
