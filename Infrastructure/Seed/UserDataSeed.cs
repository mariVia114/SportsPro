using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;

namespace SportsPro.Infrastructure.Seed
{
    public static class UserDataSeed
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

