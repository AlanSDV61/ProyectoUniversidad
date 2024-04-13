using System.ComponentModel.DataAnnotations;

namespace ProyectoUniversidad.Models
{
    public class Rol
    {
        [Key]
        public int rol_id { get; set; }

        public required string rol_nombre{ get; set; }
    }
}
