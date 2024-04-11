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
    }
}
