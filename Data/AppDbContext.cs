using Microsoft.EntityFrameworkCore;
using EmprendeLeonWeb.Models;

namespace EmprendeLeonWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprendedor> Emprendedores { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}