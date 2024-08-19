using Departments.Service_B.Persistence;
using Departments.Service_B.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(
    opt => opt.UseNpgsql(connection));

builder.Services.AddHttpClient("Service_A", client =>
{
    client.BaseAddress = new Uri("http://localhost:5082/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Access-Auth-Key", builder.Configuration["SecretKey"]);
});

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departments}/{action=Index}");

app.Run();
