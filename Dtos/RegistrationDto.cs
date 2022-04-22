namespace SportsPro.Dtos
{
    public class RegistrationDto
    {
        public int CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
    }
}
