using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class Permisos
    {
        public int IdPermiso { get; set; }
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Modulo { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Accion { get; set; }
        public Roles Roles { get; set; }
    }
}

/*
•	PermisoID (PK) – Identificador único del permiso.
•	RolID (FK) – Relación con la tabla de roles.
•	Modulo – Módulo al que se aplica el permiso.
•	Accion – Acción permitida (Leer, Crear, Actualizar, Eliminar).
*/