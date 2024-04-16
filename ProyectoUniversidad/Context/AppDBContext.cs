using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoUniversidad.Models;
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base (options)
        {
                
        }

        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Asignatura> Asignatura { get; set; }
        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Seleccion> Seleccion { get; set; }
        public DbSet<Tipo_documento> Tipo_documento { get; set; }
        public DbSet<Metodo_pago> Metodo_pago {  get; set; }
        public DbSet<Cuentas_cobrar> Cuentas_cobrar { get; set; }
        public DbSet<Factura_pago> Factura_pago { get; set; }
        public DbSet<Factura_Servicio> Factura_servicio { get; set; }
        public DbSet<Asignatura_seleccion> Asignatura_seleccion { get; set; }
        public DbSet<Pensum> Pensum { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>()
                .HasIndex(s => s.servicio_nombre)
                .IsUnique();

            modelBuilder.Entity<Carrera>()
                .HasIndex(s => s.carrera_nombre)
                .IsUnique();

            modelBuilder.Entity<Cuenta>()
                .HasIndex(s => s.cuenta_nombre)
                .IsUnique();

            //modelBuilder.Entity<Seleccion>()
            //    .Property(e => e.seleccion_estado)
            //    .HasConversion<int>();

            modelBuilder.Entity<Seleccion>()
               .Property(e => e.seleccion_estado)
               .HasConversion(new EnumToStringConverter<Estado>());

            modelBuilder.Entity<Cuentas_cobrar>()
                .Property(e => e.cuenta_estado)
                .HasConversion(new EnumToStringConverter<Estado_cuenta>());

            //PK compuestas
            modelBuilder.Entity<Pensum>()
                .HasKey(p => new { p.carrera_id, p.asignatura_id });

            modelBuilder.Entity<Asignatura_seleccion>()
                .HasKey(p => new { p.seleccion_id, p.asignatura_id });

            modelBuilder.Entity<estudianteSeleccion>()
                .HasNoKey();
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Tipo_documento>().ToTable("Tipo_documento", schema: "dbo");
        }

    }
}
