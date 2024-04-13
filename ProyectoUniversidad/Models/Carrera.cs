using System.ComponentModel.DataAnnotations;

namespace UniversidadAPI.Models
{
    public class Carrera
    {
        [Key]
        public int carrera_id { get; set; }
        public required string carrera_nombre { get; set; }
        public required string carrera_area { get; set; }
    }
}
