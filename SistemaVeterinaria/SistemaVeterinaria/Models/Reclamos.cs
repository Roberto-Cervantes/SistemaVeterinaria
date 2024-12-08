using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class Reclamos
    {
        public int IdReclamo { get; set; }
        public int IdPoliza { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public DateOnly FechaReclamo { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public bool Estado { get; set; }
        public DateOnly FechaResolucion { get; set; }

        public GestionPolizas? GestionPolizas { get; set; }
        public IEnumerable<SeguimientoReclamos> SeguimientoReclamos { get; set; } = new List<SeguimientoReclamos>();
        
    }
}

/*
 •	ReclamoID (PK) – Identificador único del reclamo.
•	PolizaID (FK) – Relación con la póliza asociada al reclamo.
•	ClienteID (FK) – Relación con el cliente que hizo el reclamo.
•	Descripcion – Descripción del siniestro.
•	FechaReclamo – Fecha en que se presentó el reclamo.
•	Estado – Estado del reclamo (Pendiente, En Proceso, Resuelto, Rechazado).
•	FechaResolucion – Fecha de resolución del reclamo (si aplica).
*/