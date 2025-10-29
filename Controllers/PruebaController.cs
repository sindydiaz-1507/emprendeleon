using Microsoft.AspNetCore.Mvc;
using EmprendeLeonWeb.Data;

namespace EmprendeLeonWeb.Controllers
{
    public class PruebaController : Controller
    {
        private readonly AppDbContext _context;

        public PruebaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var totalUsuarios = _context.Usuarios.Count(); // Consulta directa
                ViewBag.TotalUsuarios = totalUsuarios;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error de conexión: " + ex.Message;
                return View();
            }
        }
    }
}