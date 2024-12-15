using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;

namespace SistemaVeterinaria.Controllers
{
    public class SeguimientoReclamosController : Controller
    {

        private readonly ProyectContext _context;

        public SeguimientoReclamosController(ProyectContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<IActionResult> Index()
        {
            var seguimientos = await _context.SeguimientoReclamos
            .Include(s => s.Reclamos) // Incluye la relación con Reclamos
            .ToListAsync();

            return View(seguimientos);
        }

        public IActionResult Create()
        {
            var reclamos = _context.Reclamos
                .ToList();

            if (!reclamos.Any())
            {
                ViewBag.Reclamos = new List<SelectListItem>();
            }
            else
            {
                ViewBag.Reclamos = reclamos.Select(r => new SelectListItem
                {
                    Value = r.IdReclamo.ToString(),
                    Text = $"Reclamo: {r.IdReclamo} - Descripción: {r.Descripcion}"
                }).ToList();
            }
            return View(new SeguimientoReclamos());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReclamo,FechaSeguimiento,Descripcion,NotificacionCliente")] SeguimientoReclamos seguimientoreclamos)
        {
            seguimientoreclamos.Reclamos = await _context.Reclamos
            .FirstOrDefaultAsync(r => r.IdReclamo == seguimientoreclamos.IdReclamo);

            ModelState.Clear();
            TryValidateModel(seguimientoreclamos);

            if (ModelState.IsValid)
            {
                _context.Add(seguimientoreclamos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seguimientoreclamos);
        }

        #endregion

    }
}
