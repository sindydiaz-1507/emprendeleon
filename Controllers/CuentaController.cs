using Microsoft.AspNetCore.Mvc;
using EmprendeLeonWeb.Models;
using EmprendeLeonWeb.Data;
using System.Linq;

namespace EmprendeLeonWeb.Controllers
{
    public class CuentaController : Controller
    {
        private readonly AppDbContext _context;

        public CuentaController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult NuevoUsuario()
        {
            return View();
        }

        public IActionResult RegistroCliente()
        {
            return View();
        }

        public IActionResult RegistroEmprendedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarCliente(string Nombre, string Correo, string Contrasena)
        {
            // Verificar si el correo ya existe
            if (_context.Usuarios.Any(u => u.Correo == Correo))
            {
                ViewBag.Error = "Este correo ya está registrado.";
                return View("RegistroCliente");
            }

            var nuevoCliente = new Usuario
            {
                Nombre = Nombre,
                Correo = Correo,
                Contrasena = Contrasena,
                TipoUsuario = "cliente"
            };

            _context.Usuarios.Add(nuevoCliente);
            _context.SaveChanges();

            ViewBag.Mensaje = "¡Registro exitoso como cliente!";
            return View("RegistroExitoso");
        }

        [HttpPost]
        public IActionResult GuardarEmprendedor(
    string NombreUsuario,
    string Correo,
    string Contrasena,
    string NombreNegocio,
    string Ubicacion,
    string Contacto,
    string Descripcion,
    string Categoria,
    string TipoPerfil)
        {
            if (_context.Usuarios.Any(u => u.Correo == Correo))
            {
                ViewBag.Error = "Este correo ya está registrado.";
                return View("RegistroEmprendedor");
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = NombreUsuario,
                Correo = Correo,
                Contrasena = Contrasena,
                TipoUsuario = "emprendedor"
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            var nuevoEmprendedor = new Emprendedor
            {
                NombreNegocio = NombreNegocio,
                Descripcion = Descripcion,
                Ubicacion = Ubicacion,
                Contacto = Contacto,
                IdUsuario = nuevoUsuario.IdUsuario,
                Visible = true,
                Categoria = Categoria,
                TipoPerfil = TipoPerfil
            };

            _context.Emprendedores.Add(nuevoEmprendedor);
            _context.SaveChanges();

            ViewBag.Mensaje = "¡Registro exitoso como emprendedor!";
            return View("RegistroExitoso");
        }
    }
}