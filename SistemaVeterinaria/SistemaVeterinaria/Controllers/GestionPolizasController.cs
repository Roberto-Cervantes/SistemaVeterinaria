using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var gestionPolizas= await _context.GestionPolizas
                .Include(p => p.Clientes)
                .ToListAsync();
            return View(gestionPolizas);    
            //return View(await _context.GestionPolizas.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gestionpolizas = await _context.GestionPolizas
                .Include(p => p.Clientes)
                .FirstOrDefaultAsync(m => m.IdPoliza == id);
            if (gestionpolizas == null)
            {
                return NotFound();
            }

            return View(gestionpolizas);
        }


        public IActionResult Create()
        {
            var clientes =_context.Clientes.ToList();

            ViewBag.Clientes = clientes.Select(c => new SelectListItem
            {
                Value =c.IdCliente.ToString(),
                Text = c.Nombre.ToString()
            });

            return View(new GestionPolizas());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente, Categoria, FechaInicio, FechaFin, Condiciones, PrimaMensual, Estado")] 
        GestionPolizas gestionpolizas)
        {
            if (ModelState.IsValid)
            {
                _context.GestionPolizas.Add(gestionpolizas);
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

            var gestionpolizas = await _context.GestionPolizas
                .Include(p => p.Clientes)
                .FirstOrDefaultAsync(m => m.IdPoliza == id);
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
                .Include(p => p.Clientes)
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
