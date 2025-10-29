using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmprendeLeonWeb.Data;
using EmprendeLeonWeb.Models;
using System.Linq;

namespace EmprendeLeonWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Explorar(string busqueda = "")
        {
            var negocios = _context.Emprendedores
                .Where(e => e.Visible &&
                    (string.IsNullOrEmpty(busqueda) ||
                    (e.NombreNegocio ?? "").Contains(busqueda) ||
                    (e.Descripcion ?? "").Contains(busqueda) ||
                    (e.Categoria ?? "").Contains(busqueda)))
                .Include(e => e.Productos)
                .ToList();

            var agrupados = negocios
                .GroupBy(e => e.Categoria)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.Busqueda = busqueda;
            return View(agrupados);
        }

        public IActionResult Negocio(int id)
        {
            var negocio = _context.Emprendedores
                .Include(e => e.Productos)
                .FirstOrDefault(e => e.IdEmprendedor == id && e.Visible);

            if (negocio == null)
                return NotFound();

            return View(negocio);
        }
    }
}