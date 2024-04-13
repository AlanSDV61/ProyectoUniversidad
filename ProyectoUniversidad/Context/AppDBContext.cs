using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Models;
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base (options)
        {
                
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Profesor> Profesors { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Seleccion> Seleccions { get; set; }
        public DbSet<Tipo_documento> Tipo_documentos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>()
                .HasIndex(s => s.servicio_nombre)
                .IsUnique();

            modelBuilder.Entity<Carrera>()
                .HasIndex(s => s.carrera_nombre)
                .IsUnique();

            modelBuilder.Entity<Servicio>()
                .HasIndex(s => s.servicio_nombre)
                .IsUnique();

            modelBuilder.Entity<Seleccion>()
                .Property(e => e.seleccion_estado)
                .HasConversion<int>();


            base.OnModelCreating(modelBuilder);
        }

    }
}
