using Microsoft.EntityFrameworkCore;
using SistemaCatalogo.Infrastructure.Context;
using SistemaCatalogo.Infrastructure.ServiceExtensions;
using SistemaCatalogo.Application;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    
// Infraestrutura (DbContext já foi registrado acima)
builder.Services.AddInfrastructureServices();

// Serviços de Aplicação (camada Application)
builder.Services.AddApplicationServices();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); 

app.Run();