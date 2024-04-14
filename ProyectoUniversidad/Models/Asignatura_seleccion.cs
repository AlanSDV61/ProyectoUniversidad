using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Asignatura_seleccion
    {
        [Key]
        [ForeignKey("Seleccion")]
        public int seleccion_id { get; set; }

        [Key]
        [ForeignKey("Asignatura")]
        public int asignatura_id { get; set; }

        public required string calificacion { get; set; }
    }
}
