using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmprendeLeonWeb.Data;
using EmprendeLeonWeb.Models;

public class CatalogoController : Controller
{
    private readonly AppDbContext _context;

    public CatalogoController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var negocios = _context.Emprendedores
            .Where(e => e.Visible)
            .Include(e => e.Productos)
            .ToList();

        return View(negocios);
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