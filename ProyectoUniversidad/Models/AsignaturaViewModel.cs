using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class AsignaturaViewModel
    {
        public int asignatura_id { get; set; }
        public int profesor_id { get; set; }
        public string profesor_nombre { get; set; }
        public required string asignatura_nombre { get; set; }
        public required string asignatura_aula { get; set; }
        public required int asignatura_creditos { get; set; }

    }
}
