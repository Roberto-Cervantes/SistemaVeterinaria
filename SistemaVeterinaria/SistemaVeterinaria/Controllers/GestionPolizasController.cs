using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;

namespace SistemaVeterinaria.Controllers
{
    public class GestionPolizasController : Controller
    {

        private readonly ProyectContext _context;

        public GestionPolizasController(ProyectContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<IActionResult> Index()
        {
            return View(await _context.GestionPolizas.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestionpolizas = await _context.GestionPolizas
                .FirstOrDefaultAsync(m => m.IdPoliza == id);
            if (gestionpolizas == null)
            {
                return NotFound();
            }

            return View(gestionpolizas);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("")] GestionPolizasController gestionpolizas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gestionpolizas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gestionpolizas);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestionpolizas = await _context.GestionPolizas.FindAsync(id);
            if (gestionpolizas == null)
            {
                return NotFound();
            }
            return View(gestionpolizas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] GestionPolizas gestionpolizas)
        {
            if (id != gestionpolizas.IdPoliza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gestionpolizas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(gestionpolizas.IdPoliza))
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
            return View(gestionpolizas);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestionpolizas = await _context.GestionPolizas
                .FirstOrDefaultAsync(m => m.IdPoliza == id);
            if (gestionpolizas == null)
            {
                return NotFound();
            }

            return View(gestionpolizas);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gestionpolizas = await _context.GestionPolizas.FindAsync(id);
            if (gestionpolizas != null)
            {
                _context.GestionPolizas.Remove(gestionpolizas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int idPoliza)
        {
            return _context.GestionPolizas.Any(e => e.IdPoliza == idPoliza);
        }

        #endregion

    }
}
