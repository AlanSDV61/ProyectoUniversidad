using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Pensum
    {
        [Key]
        [ForeignKey("Carrera")]
        public int carrera_id { get; set; }

        [Key]
        [ForeignKey("Asignatura")]
        public int asignatura_id { get; set; }

        public int pensum_trimestre { get; set; }


    }
}
