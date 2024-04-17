using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoUniversidad.Models
{
    public enum Estado_cuenta { Pago, Pendiente}

    public class Cuentas_cobrar
    {
        [Key]
        public int cuenta_cobrar_id { get; set; }

        public required string cuenta_estado { get; set; }

        [ForeignKey("Seleccion")]
        public required int seleccion_id { get; set; }

        [ForeignKey("Estudiante")]
        public required int estudiante_id { get; set; }

        public required decimal cuenta_monto_pendiente { get; set; }

        public required decimal cuenta_monto_total { get; set; }

    }
}
