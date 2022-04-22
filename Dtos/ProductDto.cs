using System.ComponentModel.DataAnnotations;

namespace SportsPro.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Code must have atleast {2} and less than {1} characters")]
        public string? Code { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Name must have atleast {2} and less than {1} characters")]
        public string? Name { get; set; }

        [Display(Name = "Price")]
        public decimal YearlyPrice { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public IList<RegistrationDto> Registrations { get; set; } = new List<RegistrationDto>();
    }
}
