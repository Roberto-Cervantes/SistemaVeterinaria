using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaVeterinaria.Models
{
    public class ProyectContext : DbContext
    {
        public ProyectContext(DbContextOptions<ProyectContext> options) : base(options) { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<GestionPolizas> GestionPolizas { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Reclamos> Reclamos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<SeguimientoReclamos> SeguimientoReclamos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(Clientes =>{
                Clientes.HasKey(e => e.IdCliente);
                Clientes.Property(a => a.Nombre).IsRequired().HasMaxLength(64).IsUnicode(true);
                Clientes.Property(b => b.Direccion).IsRequired().HasMaxLength(256).IsUnicode(true);
                Clientes.Property(c => c.Telefono).IsRequired().HasMaxLength(16).IsUnicode(true);
                Clientes.Property(d => d.Correo).IsRequired().HasMaxLength(64).IsUnicode(true);
                Clientes.Property(f => f.FechaRegistro).IsRequired();
                Clientes.Property(g => g.Activo).IsRequired();
            });
            modelBuilder.Entity<GestionPolizas>(GestionPolizas => {
                GestionPolizas.HasKey(e => e.IdPoliza);
                GestionPolizas.Property(a => a.Categoria).IsRequired().HasMaxLength(16).IsUnicode(true);
                GestionPolizas.Property(b => b.FechaInicio).IsRequired();
                GestionPolizas.Property(c => c.FechaFin).IsRequired();
                GestionPolizas.Property(d => d.Condiciones).IsRequired().HasMaxLength(1024).IsUnicode(true);
                GestionPolizas.Property(f => f.PrimaMensual).IsRequired();
                GestionPolizas.Property(g => g.Estado).IsRequired();
            });
            modelBuilder.Entity<Permisos>(Permisos => {
                Permisos.HasKey(e => e.IdPermiso);
                Permisos.Property(a => a.Modulo).IsRequired().HasMaxLength(64).IsUnicode(true);
                Permisos.Property(b => b.Accion).IsRequired().HasMaxLength(32).IsUnicode(true);
            });
            modelBuilder.Entity<Reclamos>(Reclamos => {
                Reclamos.HasKey(e => e.IdReclamo);
                Reclamos.Property(a => a.Descripcion).IsRequired().HasMaxLength(1024).IsUnicode(true);
                Reclamos.Property(b => b.FechaReclamo).IsRequired();
                Reclamos.Property(c => c.Estado).IsRequired();
                Reclamos.Property(d => d.FechaResolucion).IsRequired();
            });
            modelBuilder.Entity<Roles>(Roles => {
                Roles.HasKey(e => e.IdRol);
                Roles.Property(a => a.NombreRol).IsRequired().HasMaxLength(64).IsUnicode(true);
                Roles.Property(b => b.Descripcion).IsRequired().HasMaxLength(512).IsUnicode(true);
            });
            modelBuilder.Entity<SeguimientoReclamos>(SeguimientoReclamos => {
                SeguimientoReclamos.HasKey(e => e.IdSeguimiento);
                SeguimientoReclamos.Property(a => a.FechaSeguimiento).IsRequired();
                SeguimientoReclamos.Property(b => b.Descripcion).IsRequired().HasMaxLength(512).IsUnicode(true);
                SeguimientoReclamos.Property(c => c.NotificacionCliente).IsRequired();
            });
            modelBuilder.Entity<Usuarios>(Usuarios => {
                Usuarios.HasKey(e => e.IdUsuario);
                Usuarios.Property(a => a.nombreUsuario).IsRequired().HasMaxLength(64).IsUnicode(true);
                Usuarios.Property(b => b.Password).IsRequired().HasMaxLength(256).IsUnicode(true);
                Usuarios.Property(c => c.Rol).IsRequired().HasMaxLength(32).IsUnicode(true);
                Usuarios.Property(d => d.EstadoUsuario).IsRequired();
            });

            modelBuilder.Entity<GestionPolizas>().HasOne(x => x.Clientes).WithMany(e => e.GestionPolizas).HasForeignKey(f => f.IdCliente);
            modelBuilder.Entity<Reclamos>().HasOne(x => x.GestionPolizas).WithMany(e => e.Reclamos).HasForeignKey(f => f.IdPoliza);
            modelBuilder.Entity<SeguimientoReclamos>().HasOne(x => x.Reclamos).WithMany(e => e.SeguimientoReclamos).HasForeignKey(f => f.IdReclamo);
            modelBuilder.Entity<Permisos>().HasOne(x => x.Roles).WithMany(e => e.Permisos).HasForeignKey(f => f.IdRol);
        }
    }
}
