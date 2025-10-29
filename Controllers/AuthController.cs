using Microsoft.AspNetCore.Mvc;
using EmprendeLeonWeb.Data;
using EmprendeLeonWeb.Models;

namespace EmprendeLeonWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string correo, string contrasena)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == correo && u.Contrasena == contrasena);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuario ?? "");
                HttpContext.Session.SetString("NombreUsuario", usuario.Nombre ?? "");


                // Redirigir según rol
                if (usuario.TipoUsuario == "admin")
                    return RedirectToAction("Panel", "Admin");
                else if (usuario.TipoUsuario == "emprendedor")
                    return RedirectToAction("MisProductos", "Emprendedor");
                else
                    return RedirectToAction("Explorar", "Cliente");
            }

            ViewBag.Error = "Correo o contraseña incorrectos.";
            return View();
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}