using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;

namespace SistemaVeterinaria.Controllers
{
    public class PermisosController : Controller
    {

        private readonly ProyectContext _context;

        public PermisosController(ProyectContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<IActionResult> Index()
        { 
            var permisos= await _context.Permisos
            .Include(p=>p.Roles)
            .ToListAsync();
            return View(permisos);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisos = await _context.Permisos
                .Include(p=>p.Roles)    
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (permisos == null)
            {
                return NotFound();
            }

            return View(permisos);
        }


        public IActionResult Create()
        {
            var roles = _context.Roles.ToList();
            ViewBag.Roles = roles.Select(r => new SelectListItem { 
                Value =r.IdRol.ToString(),
                Text= r.NombreRol.ToString()
            });
            return View(new Permisos());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRol, Modulo, Accion")] Permisos permiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.ToListAsync();
            if (!roles.Any())
            {
                throw new Exception("La tabla Roles está vacía");
            }

            ViewBag.Roles = new SelectList(await _context.Roles.ToListAsync(), "IdRol", "NombreRol");

            var permisos = await _context.Permisos
                .Include(p => p.Roles)
                .FirstOrDefaultAsync(m=>m.IdPermiso == id);

            if (permisos == null)
            {
                return NotFound();
            }
            return View(permisos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] Permisos permiso)
        {
            if (id != permiso.IdPermiso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(permiso.IdPermiso))
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
            return View(permiso);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisos = await _context.Permisos
                .Include(g=>g.Roles)
                .FirstOrDefaultAsync(m => m.IdPermiso == id);
            if (permisos == null)
            {
                return NotFound();
            }

            return View(permisos);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permisos = await _context.Permisos.FindAsync(id);
            if (permisos != null)
            {
                _context.Permisos.Remove(permisos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int idPermiso)
        {
            return _context.Permisos.Any(e => e.IdPermiso == idPermiso);
        }

        #endregion

    }
}
