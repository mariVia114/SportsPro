using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Infrastructure.Interfaces;
using SportsPro.Infrastructure.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddSession();
var connectionString = builder.Configuration.GetConnectionString("SportsProIdentityDbContextConnection");
services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(connectionString));
services.AddScoped<ICountryRepository, CountryRepository>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IIncidentRepository, IncidentRepository>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<ITechnicianRepository, TechnicianRepository>();
services.AddScoped<IRegistrationRepository, RegistrationRepository>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddRouting(options => options.LowercaseUrls = true); //lowercase all urls
// Register the Auto Mapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddControllersWithViews();
services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

services.AddAuthentication(o =>
{
    o.DefaultScheme = IdentityConstants.ApplicationScheme;
    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddCookie("Identity.Application", o =>
{
    o.LoginPath = "/account/login";
    o.AccessDeniedPath = "/account/accessdenied";
    o.LogoutPath = "/account/logoff";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.InitializeDatabase().Run();