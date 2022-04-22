using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportsPro.Data.Configurations
{
    public class CategoriesConfiguration
        : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(reg => new { reg.CustomerId, reg.ProductId });
        }
    }


}
