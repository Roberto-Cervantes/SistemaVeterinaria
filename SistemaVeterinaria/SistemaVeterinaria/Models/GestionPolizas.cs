using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class GestionPolizas
    {
        public int IdPoliza { get; set; }
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Categoria { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public DateOnly FechaInicio { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public DateOnly FechaFin { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Condiciones { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public Double PrimaMensual { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Estado { get; set; }

        public Clientes Clientes { get; set; }
        public IEnumerable<Reclamos> Reclamos { get; set; } = new List<Reclamos>();
    }
}
/*
•	PolizaID(PK) – Identificador único de la póliza.
•	ClienteID (FK) – Relación con el cliente propietario de la póliza.
•	Categoria – Categoría del seguro (automóvil, hogar, salud, etc.).
•	FechaInicio – Fecha de inicio de la póliza.
•	FechaFin – Fecha de vencimiento de la póliza.
•	TerminosCondiciones – Términos y condiciones de la póliza.
•	PrimaMensual – Monto mensual de la prima.
•	Estado – Estado de la póliza (Vigente, Expirada, Cancelada).
*/
