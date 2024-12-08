using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;
using System.Data;

namespace SistemaVeterinaria.Controllers
{
    public class ReclamosController : Controller
    {

        private readonly ProyectContext _context;

        public ReclamosController(ProyectContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<IActionResult> Index()
        {
            var reclamos = await _context.Reclamos
                .Include(p => p.GestionPolizas)
                .Include(g => g.GestionPolizas.Clientes)
                .ToListAsync();
            return View(reclamos);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamos = await _context.Reclamos
                .Include(g => g.GestionPolizas.Clientes)
                .FirstOrDefaultAsync(m => m.IdReclamo == id);
            if (reclamos == null)
            {
                return NotFound();
            }

            return View(reclamos);
        }


        public IActionResult Create()
        {
            var gestionpolizas = _context.GestionPolizas
                .Include(g => g.Clientes)
                .ToList();
            if (!gestionpolizas.Any())
            {
                ViewBag.GestionPolizas = new List<SelectListItem>();
            }
            else
            {
                ViewBag.GestionPolizas = gestionpolizas.Select(r => new SelectListItem
                {
                    Value = r.IdPoliza.ToString(),
                    Text = r.Clientes.Nombre.ToString()
                }).ToList();
            }
            return View(new Reclamos());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPoliza, Descripcion, FechaReclamo, Estado, FechaResolucion")] Reclamos reclamos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reclamos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reclamos);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestionpolizas = await _context.GestionPolizas.ToListAsync();
            if (!gestionpolizas.Any())
            {
                throw new Exception("La tabla Gestion Polizas está vacía");
            }
            ViewBag.GestionPolizas = new SelectList(await _context.GestionPolizas.ToListAsync(), "IdPoliza", "Condiciones");

            var reclamos = await _context.Reclamos
                .Include(x => x.GestionPolizas)
                .Include(c => c.GestionPolizas.Clientes)
                .FirstOrDefaultAsync(m=> m.IdReclamo == id);
            if (reclamos == null)
            {
                return NotFound();
            }
            return View(reclamos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReclamo,IdPoliza,Descripcion,FechaReclamo,Estado,FechaResolucion")] Reclamos reclamos)
        {
            if (id != reclamos.IdReclamo)
            {   
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reclamos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(reclamos.IdReclamo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reclamos);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamos = await _context.Reclamos
                .Include(g=>g.GestionPolizas)
                .Include(g => g.GestionPolizas.Clientes)
                .FirstOrDefaultAsync(m => m.IdReclamo == id);
            if (reclamos == null)
            {
                return NotFound();
            }

            return View(reclamos);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamos = await _context.Reclamos.FindAsync(id);
            if (reclamos != null)
            {
                _context.Reclamos.Remove(reclamos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int idReclamos)
        {
            return _context.Reclamos.Any(e => e.IdReclamo == idReclamos);
        }

        #endregion

    }
}
