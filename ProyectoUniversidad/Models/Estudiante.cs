using System.ComponentModel.DataAnnotations;

namespace ProyectoUniversidad.Models
{
    public class Estudiante
    {
        [Key]
        public required int estudiante_id { get; set; }

        public required string estudiante_nombre { get; set; }

        public required string estudiante_apellido { get; set; }

        public required int carrera_id { get; set; }

        public decimal estudiante_indice { get; set; }

        public required string estudiante_telefono { get; set; }

        public required string estudiante_correo { get; set; }

        public required int cuenta_id { get; set; }

        public required string estudiante_nacionalidad { get; set; }

        public required int estudiante_trimestre { get; set; }

        public required int tipo_documento_id { get; set; }

        public required string estudiante_documento {  get; set; }

        public required DateOnly estudiante_fecha_ingreso { get; set; }

        public required bool estudiante_activo { get; set; }

    }
}
