using ProyectoUniversidad.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadAPI.Models
{
    public class Asignatura
    {
        [Key]
        public int asignatura_id { get; set; }

        [ForeignKey("Profesor")]
        public int profesor_id { get; set; }
        //public Profesor? Profesor { get; set; }
        public required string asignatura_nombre { get; set; }
        public required string asignatura_aula {  get; set; }
        public required int asignatura_creditos { get; set; }

    }
}
