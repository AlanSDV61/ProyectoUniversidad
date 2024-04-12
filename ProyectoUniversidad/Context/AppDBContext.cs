using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Models;

namespace ProyectoUniversidad.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base (options)
        {
                
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>()
                .HasIndex(s => s.servicio_nombre)
                .IsUnique();
        }
    }
}
