using Microsoft.EntityFrameworkCore;
using SportsPro.Data;

namespace SportsPro.Infrastructure.Seed
{
    public static class CountryDataSeed
    {
        public static IHost InitializeDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();

                        var countries = context.Countries.ToList();
                        if (!countries.Any())
                        {
                            countries = new List<Country>
                            {
                                new Country{Name = "United States", Abbr = "US"},
                                new Country{Name = "United Kingdom", Abbr = "UK"},
                                new Country{Name = "Canada", Abbr = "CA"},
                            };

                            context.Countries.AddRange(countries);
                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            return host;
        }
    }
}

