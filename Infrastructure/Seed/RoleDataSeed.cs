using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;

namespace SportsPro.Infrastructure.Seed
{
    public static class RoleDataSeed
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

