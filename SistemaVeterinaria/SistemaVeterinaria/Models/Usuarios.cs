using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String nombreUsuario  { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Rol { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String EstadoUsuario { get; set; }
    }
}

/*
•	UsuarioID (PK) – Identificador único del usuario.
•	NombreUsuario – Nombre de usuario.
•	Password – Contraseña encriptada.
•	Rol – Rol del usuario (Administrador, Agente, Cliente, etc.).
•	Activo – Estado del usuario (1: Activo, 0: Inactivo).
*/