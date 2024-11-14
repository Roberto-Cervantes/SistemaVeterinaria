using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class SeguimientoReclamos
    {
        public int IdSeguimiento { get; set; }
        public int IdReclamo { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public DateOnly FechaSeguimiento { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "Requerido")]  
        public bool NotificacionCliente { get; set; }

        public Reclamos Reclamos { get; set; }
    }
}

/*
•	SeguimientoID (PK) – Identificador único del seguimiento.
•	ReclamoID (FK) – Relación con el reclamo asociado.
•	FechaSeguimiento – Fecha del seguimiento.
•	Descripcion – Descripción del seguimiento (acciones tomadas).
•	NotificacionCliente – (1: Notificado, 0: No Notificado).
*/