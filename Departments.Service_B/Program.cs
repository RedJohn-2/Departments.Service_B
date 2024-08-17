using Departments.Service_B.Persistence;
using Departments.Service_B.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(
    opt => opt.UseNpgsql(connection));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departments}/{action=Index}");

app.Run();
