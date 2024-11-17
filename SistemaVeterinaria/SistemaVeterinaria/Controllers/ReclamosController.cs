using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;

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
            return View(await _context.Reclamos.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamos = await _context.Reclamos
                .FirstOrDefaultAsync(m => m.IdReclamo == id);
            if (reclamos == null)
            {
                return NotFound();
            }

            return View(reclamos);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("")] Reclamos reclamos)
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

            var reclamos = await _context.Reclamos.FindAsync(id);
            if (reclamos == null)
            {
                return NotFound();
            }
            return View(reclamos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] Reclamos reclamos)
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
