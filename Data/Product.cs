using System.ComponentModel.DataAnnotations;

namespace SportsPro.Data
{
    public class Product
    {
        public int ProductId { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Code must have atleast {2} and less than {1} characters")]
        public string Code { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Name must have atleast {2} and less than {1} characters")]
        public string Name { get; set; }

        public decimal YearlyPrice { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IList<Registration> Registrations { get; set; } = new List<Registration>();

    }
}
