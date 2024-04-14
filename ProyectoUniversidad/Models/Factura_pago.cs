using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public class Factura_pago
    {
        [Key]
        public required int pago_id { get; set; }

        [ForeignKey("Cuentas_cobrar")]
        public required int cuenta_cobrar_id { get; set; }

        [ForeignKey("Metodo_pago")]
        public required int metodo_pago_id{ get; set; }

        public required decimal factura_monto { get; set; }

        public required DateOnly fecha_factura { get; set; }
    }
}
