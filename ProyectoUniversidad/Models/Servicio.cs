using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Servicio
    {
        [Key]
        public int servicio_id { get; set; }

        public required string servicio_nombre { get; set; }

        public required decimal servicio_costo { get; set; }
    }
}
