using Microsoft.AspNetCore.Mvc;
using EmprendeLeonWeb.Data;
using EmprendeLeonWeb.Models;
using EmprendeLeonWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EmprendeLeonWeb.Controllers
{
    public class EmprendedorController : Controller
    {
        private readonly AppDbContext _context;

        public EmprendedorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult MisProductos()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
                return RedirectToAction("Login", "Auth");

            var emprendedor = _context.Emprendedores
                .FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (emprendedor == null)
                return RedirectToAction("RegistrarNegocio");

            var productos = _context.Productos
                .Where(p => p.IdEmprendedor == emprendedor.IdEmprendedor)
                .ToList();

            if (productos.Count == 0)
                return View("SinProductos");

            return View(productos);
        }

        [HttpGet]
        public IActionResult RegistrarNegocio()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Auth");

            var yaTieneNegocio = _context.Emprendedores.Any(e => e.IdUsuario == idUsuario);
            if (yaTieneNegocio)
            {
                TempData["Mensaje"] = "Ya tienes un negocio registrado.";
                return RedirectToAction("MisProductos");
            }

            return View();
        }

        [HttpPost]
        public IActionResult RegistrarNegocio(Emprendedor emprendedor)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Auth");

            emprendedor.IdUsuario = idUsuario.Value;
            emprendedor.Visible = true;
            _context.Emprendedores.Add(emprendedor);
            _context.SaveChanges();

            TempData["Mensaje"] = "Negocio registrado exitosamente.";
            return RedirectToAction("MisProductos");
        }

        [HttpPost]
        public IActionResult EliminarNegocio()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Auth");

            var negocio = _context.Emprendedores
                .FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (negocio != null)
            {
                var productos = _context.Productos
                    .Where(p => p.IdEmprendedor == negocio.IdEmprendedor)
                    .ToList();

                _context.Productos.RemoveRange(productos);
                _context.Emprendedores.Remove(negocio);
                _context.SaveChanges();

                TempData["Mensaje"] = "Negocio eliminado correctamente.";
            }

            return RedirectToAction("Explorar", "Cliente");
        }

        [HttpGet]
        public IActionResult RegistroCompleto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistroCompleto(RegistroCompletoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = new Usuario
            {
                Nombre = model.NombreUsuario ?? "",
                Correo = model.Correo ?? "",
                Contrasena = model.Contrasena ?? ""
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            var negocio = new Emprendedor
            {
                NombreNegocio = model.NombreNegocio ?? "",
                Descripcion = model.Descripcion ?? "",
                Ubicacion = model.Ubicacion ?? "",
                Contacto = model.Contacto ?? "",
                Categoria = model.Categoria ?? "General",
                Visible = true,
                IdUsuario = usuario.IdUsuario
            };

            _context.Emprendedores.Add(negocio);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
            HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);

            TempData["Mensaje"] = "Registro completo exitoso.";
            return RedirectToAction("MisProductos");
        }

        [HttpGet]
        public IActionResult PerfilNegocio(int id)
        {
            var negocio = _context.Emprendedores
                .FirstOrDefault(e => e.IdEmprendedor == id && e.Visible == true);

            if (negocio == null)
                return NotFound();

            var productos = _context.Productos
                .Where(p => p.IdEmprendedor == id && p.Disponibilidad == "disponible")
                .Select(p => new ProductoViewModel
                {
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion ?? "",
                    Precio = p.Precio,
                    Imagen = p.Imagen ?? "/img/default.png"
                }).ToList();

            var modelo = new PerfilNegocioViewModel
            {
                IdEmprendedor = negocio.IdEmprendedor,
                NombreNegocio = negocio.NombreNegocio ?? "",
                Descripcion = negocio.Descripcion ?? "",
                Ubicacion = negocio.Ubicacion ?? "",
                Contacto = negocio.Contacto ?? "",
                Categoria = negocio.Categoria ?? "General",
                Productos = productos
            };

            return View(modelo);
        }
    }
}