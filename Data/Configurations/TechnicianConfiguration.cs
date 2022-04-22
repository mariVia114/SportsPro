using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportsPro.Data.Configurations
{
    public class TechnicianConfiguration
       : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasKey(x => x.TechnicianId);
            builder.HasMany(x => x.Incidents).WithOne(x => x.Technician).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
