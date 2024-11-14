using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class Roles
    {
        public int IdRol { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String NombreRol { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Descripcion { get; set; }

        public IEnumerable<Permisos> Permisos { get; set; } = new List<Permisos>();

    }
}

/*
•	RolID (PK) – Identificador único del rol.
•	NombreRol – Nombre del rol (Administrador, Cliente, etc.).
•	Descripcion – Descripción del rol.
*/