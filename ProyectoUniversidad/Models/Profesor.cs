using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Models
{
    public class Profesor
    {
        [Key]
        public int profesor_id { get; set; }

        public required string profesor_nombres { get; set; }

        public required string profesor_apellidos { get; set; }

        [ForeignKey("Tipo_documento")]
        public required int tipo_documento_id { get; set; }
        //public Tipo_documento Tipo_documento { get; set; }

        public required string profesor_documento { get; set; }

        [ForeignKey("Cuenta")]
        public required int id_cuenta { get; set; }
        //public Cuenta Cuenta { get; set; }
    }
}
