using System.ComponentModel.DataAnnotations;

namespace SistemaVeterinaria.Models
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Direccion { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Telefono { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public String Correo { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public DateOnly FechaRegistro { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public bool Activo { get; set; }

        public IEnumerable<GestionPolizas> GestionPolizas { get; set; } = new List<GestionPolizas>();
    }
}

/*
•	ClienteID (PK) – Identificador único del cliente.
•	Nombre – Nombre completo del cliente.
•	Direccion – Dirección del cliente.
•	Telefono – Número de teléfono.
•	Email – Correo electrónico.
•	FechaRegistro – Fecha en la que se registró el cliente.
•	Activo – Estado del cliente (1: Activo, 0: Inactivo).
*/