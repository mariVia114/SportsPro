using System.ComponentModel.DataAnnotations;

namespace SportsPro.Dtos
{
    public class TechnicianDto
    {
        public int TechnicianId { get; set; }


        [StringLength(51, MinimumLength = 1, ErrorMessage = "Name must have atleast {2} and less than {1} characters")]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        //[RegularExpression(@"^[0-9]{3}-[0-9]{4}-[0-9]{4}$", ErrorMessage = "Phone number must be in the 999-999-9999 format")]
        [Phone]
        public string? Phone { get; set; }
    }
}
