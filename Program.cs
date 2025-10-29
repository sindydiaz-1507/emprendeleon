using EmprendeLeonWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Conexión a MariaDB (XAMPP)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MariaDbConnection"),
        new MySqlServerVersion(new Version(10, 4, 27)) // Ajusta según tu versión
    ));

// Servicios MVC + sesiones
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Activar sesiones
app.UseAuthorization();

// Ruta principal
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();