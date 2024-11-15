using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinaria.Models;

namespace SistemaVeterinaria.Controllers
{
    public class ClientesController : Controller
    {


        private readonly ProyectContext _context;

        public ClientesController(ProyectContext context)
        {
            _context = context;
        }

        #region CRUD
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente, Nombre, Direccion, Telefono," +
            "Correo, FechaRegistro, Activo")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientes);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente, Nombre, Direccion, Telefono," +
            "Correo, FechaRegistro, Activo")] Clientes clientes)
        {
            if (id != clientes.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(clientes.IdCliente))
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
            return View(clientes);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes != null)
            {
                _context.Clientes.Remove(clientes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int idClientes)
        {
            return _context.Clientes.Any(e => e.IdCliente == idClientes);
        }

        #endregion




    }
}
