using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsPro.Data
{
    public static class DbInitializer
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

                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        var adminRoleExist = roleManager.RoleExistsAsync(Role.ADMIN).Result;
                        if (!adminRoleExist)
                        {
                            var adminRole = new IdentityRole { Name = Role.ADMIN };
                            var adminRoleResult = roleManager.CreateAsync(adminRole).Result;
                        }

                        var technicianRoleExist = roleManager.RoleExistsAsync(Role.TECHNICIAN).Result;
                        if (!technicianRoleExist)
                        {
                            var technicianRole = new IdentityRole { Name = Role.TECHNICIAN };
                            var technicianRoleResult = roleManager.CreateAsync(technicianRole).Result;
                        }

                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                        var admin = userManager.FindByNameAsync("admin").Result;
                        if (admin == null)
                        {
                            admin = new ApplicationUser
                            {
                                Email = "admin@admin.com",
                                UserName = "admin"
                            };

                            var createAdminResult = userManager.CreateAsync(admin, "password").Result;
                            if (createAdminResult.Succeeded)
                            {
                                var addRoleToAdminResult = userManager.AddToRoleAsync(admin, Role.ADMIN).Result;
                            }
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