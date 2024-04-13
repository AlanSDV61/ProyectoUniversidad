using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Cuenta
    {
        [Key]
        public int cuenta_id { get; set; }

        public required string cuenta_nombre { get; set; }

        public required string cuenta_clave { get; set; }

        [ForeignKey("Rol")]
        public required int rol_id { get; set; }
        //public Rol Rol { get; set; }

    }
}
