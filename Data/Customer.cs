using System.ComponentModel.DataAnnotations;

namespace SportsPro.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "First name must have atleast {2} and less than {1} characters")]
        public string FirstName { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Last name must have atleast {2} and less than {1} characters")]
        public string LastName { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Address must have atleast {2} and less than {1} characters")]
        public string Address { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "City must have atleast {2} and less than {1} characters")]
        public string City { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "State must have atleast {2} and less than {1} characters")]
        public string State { get; set; }

        [StringLength(21, MinimumLength = 1, ErrorMessage = "Postal Code must have atleast {2} and less than {1}  characters")]
        public string PostalCode { get; set; }

        public int CountryId { get; set; }

        public Country? Country { get; set; }

        [StringLength(51, MinimumLength = 1, ErrorMessage = "Must have atleast {2} and less than {1} characters")]
        public string? Email { get; set; }

        [RegularExpression(@"^[0 - 9]{3}-[0-9]{4}-[0 - 9]{ 4}$", ErrorMessage = "Phone number must be in the 999-999-9999 format")]
        public string? Phone { get; set; }

        public string FullName
        {
            get { return $"{FirstName}  {LastName}"; }
        }

        public IList<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
