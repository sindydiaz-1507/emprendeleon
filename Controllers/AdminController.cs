using Microsoft.AspNetCore.Mvc;
using EmprendeLeonWeb.Data;
using EmprendeLeonWeb.Models;
using System.Linq;

namespace EmprendeLeonWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Panel()
        {
            var usuarios = _context.Usuarios.ToList();
            var negocios = _context.Emprendedores.ToList();

            var lista = usuarios.Select(u => new UsuarioConNegocio
            {
                Usuario = u,
                Negocio = negocios.FirstOrDefault(n => n.IdUsuario == u.IdUsuario)
            }).ToList();

            return View(lista);
        }

        [HttpPost]
        public IActionResult CambiarVisibilidad(int id)
        {
            var negocio = _context.Emprendedores.FirstOrDefault(e => e.IdEmprendedor == id);
            if (negocio != null)
            {
                negocio.Visible = !negocio.Visible;
                _context.SaveChanges();
            }

            return RedirectToAction("Panel");
        }
    }
}