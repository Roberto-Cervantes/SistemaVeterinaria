using Microsoft.AspNetCore.Mvc;
using SistemaVeterinaria.Models;
using System.Collections.Generic;

namespace SistemaVeterinaria.Controllers
{
    public class PersonalizarSeguroController : Controller
    {
        private readonly ProyectContext _context;

        public PersonalizarSeguroController(ProyectContext context)
        {
            _context = context;
        }

        // Vista principal para personalizar seguro
        public IActionResult Index()
        {
            ViewBag.Coberturas = new List<string> {
                "Accidentes Personales (+$10)",
                "Robo (+$15)",
                "Daños Materiales (+$20)",
                "Asistencia en el Extranjero (+$25)"
            };

            var clientes = _context.Clientes.ToList();
            ViewBag.Clientes = clientes;
            return View();
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(string categoria, int clienteId, int periodo, List<string> coberturas)
        {
            if (SessionHelper.Carrito == null)
                SessionHelper.Carrito = new List<SeguroPersonalizado>();

            var seguro = new SeguroPersonalizado
            {
                ClienteId = clienteId,
                Categoria = categoria,
                Periodo = periodo,
                CoberturasAdicionales = coberturas,
                CostoTotal = CalcularCostoBase() + CalcularCostoCoberturas(coberturas, periodo)
            };

            SessionHelper.Carrito.Add(seguro);
            return RedirectToAction("Carrito");
        }

        public IActionResult Carrito()
        {
            var carrito = SessionHelper.Carrito ?? new List<SeguroPersonalizado>();
            return View(carrito);
        }

        private double CalcularCostoBase() => 50.0; // Ejemplo de costo base
        private double CalcularCostoCoberturas(List<string> coberturas, int periodo)
        {
            double total = 0;
            foreach (var c in coberturas)
            {
                total += c.Contains("Accidentes") ? 10 : c.Contains("Robo") ? 15 : c.Contains("Materiales") ? 20 : 25;
            }
            return total * periodo;
        }
    }

    public static class SessionHelper
    {
        public static List<SeguroPersonalizado> Carrito { get; set; }
    }

    public class SeguroPersonalizado
    {
        public int ClienteId { get; set; }
        public string Categoria { get; set; }
        public int Periodo { get; set; }
        public List<string> CoberturasAdicionales { get; set; }
        public double CostoTotal { get; set; }
    }
}
