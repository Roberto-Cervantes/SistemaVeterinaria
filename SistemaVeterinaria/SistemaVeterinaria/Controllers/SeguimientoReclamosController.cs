using Microsoft.AspNetCore.Mvc;
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
            return View(await _context.SeguimientoReclamos.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientoreclamos = await _context.SeguimientoReclamos
                .FirstOrDefaultAsync(m => m.IdSeguimiento  == id);
            if (seguimientoreclamos == null)
            {
                return NotFound();
            }

            return View(seguimientoreclamos);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("")] SeguimientoReclamosController seguimientoreclamos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguimientoreclamos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seguimientoreclamos);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientoreclamos = await _context.SeguimientoReclamos.FindAsync(id);
            if (seguimientoreclamos == null)
            {
                return NotFound();
            }
            return View(seguimientoreclamos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] SeguimientoReclamos seguimientoreclamos)
        {
            if (id != seguimientoreclamos.IdSeguimiento )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguimientoreclamos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(seguimientoreclamos.IdSeguimiento ))
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
            return View(seguimientoreclamos);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguimientoreclamos = await _context.SeguimientoReclamos
                .FirstOrDefaultAsync(m => m.IdSeguimiento  == id);
            if (seguimientoreclamos == null)
            {
                return NotFound();
            }

            return View(seguimientoreclamos);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguimientoreclamos = await _context.SeguimientoReclamos.FindAsync(id);
            if (seguimientoreclamos != null)
            {
                _context.SeguimientoReclamos.Remove(seguimientoreclamos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int IdSeguimiento )
        {
            return _context.SeguimientoReclamos.Any(e => e.IdSeguimiento  == IdSeguimiento );
        }

        #endregion

    }
}
