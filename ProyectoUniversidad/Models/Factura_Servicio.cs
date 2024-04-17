using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Factura_Servicio
    {
        [Key]
        public int factura_id { get; set; }

        [ForeignKey("Servicio")]
        public required int servicio_id{ get; set; }

        [ForeignKey("Estudiante")]
        public required int estudiante_id { get; set; }

        public required DateTime factura_fecha { get; set; }

        [ForeignKey("Metodo_pago")]
        public required int metodo_pago_id { get; set; }

        public required decimal factura_monto { get; set; }

    }
}
