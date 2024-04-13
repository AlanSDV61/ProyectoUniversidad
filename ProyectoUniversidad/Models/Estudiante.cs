using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Models
{
    public class Estudiante
    {
        [Key]
        public required int estudiante_id { get; set; }

        public required string estudiante_nombre { get; set; }

        public required string estudiante_apellido { get; set; }

        [ForeignKey("Carrera")]
        public required int carrera_id { get; set; }
        //public Carrera Carrera { get; set; }

        public decimal estudiante_indice { get; set; }

        public required string estudiante_telefono { get; set; }

        public required string estudiante_correo { get; set; }

        [ForeignKey("Cuenta")]
        public required int cuenta_id { get; set; }
        //public Cuenta Cuenta { get; set; }

        public required string estudiante_nacionalidad { get; set; }

        public required int estudiante_trimestre { get; set; }

        [ForeignKey("Tipo_documento")]
        public required int tipo_documento_id { get; set; }
        //public Tipo_documento Tipo_documento { get; set; }

        public required string estudiante_documento {  get; set; }

        public required DateOnly estudiante_fecha_ingreso { get; set; }

        public required bool estudiante_activo { get; set; }

    }
}
