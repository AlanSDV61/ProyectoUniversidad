using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class PK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //FacturaPago
            migrationBuilder.CreateIndex(
                name: "IX_Factura_pago_cuenta_cobrar_id",
                table: "Factura_pago",
                column: "cuenta_cobrar_id");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_pago_metodo_pago_id",
                table: "Factura_pago",
                column: "metodo_pago_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_pago_Cuentas_cobrar_cuenta_cobrar_id",
                table: "Factura_pago",
                column: "cuenta_cobrar_id",
                principalTable: "Cuentas_cobrar",
                principalColumn: "cuenta_cobrar_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_pago_Metodo_pago_metodo_pago_id",
                table: "Factura_pago",
                column: "metodo_pago_id",
                principalTable: "Metodo_pago",
                principalColumn: "metodo_pago_id",
                onDelete: ReferentialAction.Cascade);

            //Cuentas cobrar
            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_cobrar_seleccion_id",
                table: "Cuentas_cobrar",
                column: "seleccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_cobrar_estudiante_id",
                table: "Cuentas_cobrar",
                column: "estudiante_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_cobrar_Seleccions_seleccion_id",
                table: "Cuentas_cobrar",
                column: "seleccion_id",
                principalTable: "Seleccions",
                principalColumn: "seleccion_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_cobrar_Estudiantes_estudiante_id",
                table: "Cuentas_cobrar",
                column: "estudiante_id",
                principalTable: "Estudiantes",
                principalColumn: "estudiante_id",
                onDelete: ReferentialAction.Cascade);

            //factura servicio
            migrationBuilder.CreateIndex(
                name: "IX_Factura_Servicio_servicio_id",
                table: "Factura_Servicio",
                column: "servicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Servicio_estudiante_id",
                table: "Factura_Servicio",
                column: "estudiante_id");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Servicio_metodo_pago_id",
                table: "Factura_Servicio",
                column: "metodo_pago_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Servicio_Estudiantes_estudiante_id",
                table: "Factura_Servicio",
                column: "estudiante_id",
                principalTable: "Estudiantes",
                principalColumn: "estudiante_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Servicio_Metodo_pago_metodo_pago_id",
                table: "Factura_Servicio",
                column: "metodo_pago_id",
                principalTable: "Metodo_pago",
                principalColumn: "metodo_pago_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Servicio_Servicios_servicio_id",
                table: "Factura_Servicio",
                column: "servicio_id",
                principalTable: "Servicios",
                principalColumn: "servicio_id",
                onDelete: ReferentialAction.Cascade);

            //asignatura seleccion
            migrationBuilder.CreateIndex(
                name: "IX_Asignatura_seleccion_seleccion_id",
                table: "Asignatura_seleccion",
                column: "seleccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Asignatura_seleccion_asignatura_id",
                table: "Asignatura_seleccion",
                column: "asignatura_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignatura_seleccion_Seleccions_seleccion_id",
                table: "Asignatura_seleccion",
                column: "seleccion_id",
                principalTable: "Seleccions",
                principalColumn: "seleccion_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignatura_seleccion_Asignaturas_asignatura_id",
                table: "Asignatura_seleccion",
                column: "asignatura_id",
                principalTable: "Asignaturas",
                principalColumn: "asignatura_id",
                onDelete: ReferentialAction.Cascade);

            //pensum
            migrationBuilder.CreateIndex(
               name: "IX_Pensum_carrera_id",
               table: "Pensum",
               column: "carrera_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pensum_asignatura_id",
                table: "Pensum",
                column: "asignatura_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pensum_Carreras_carrera_id",
                table: "Pensum",
                column: "carrera_id",
                principalTable: "Carreras",
                principalColumn: "carrera_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pensum_Asignaturas_asignatura_id",
                table: "Pensum",
                column: "asignatura_id",
                principalTable: "Asignaturas",
                principalColumn: "asignatura_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Factura pago
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_pago_Cuentas_cobrar_cuenta_cobrar_id",
                table: "Factura_pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Factura_pago_Metodo_pago_metodo_pago_id",
                table: "Factura_pago");

            migrationBuilder.DropIndex(
                name: "IX_Factura_pago_cuenta_cobrar_id",
                table: "Factura_pago");

            migrationBuilder.DropIndex(
                name: "IX_Factura_pago_metodo_pago_id",
                table: "Factura_pago");
     
            //Cuentas cobrar
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_cobrar_Seleccions_seleccion_id",
                table: "Cuentas_cobrar");

            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_cobrar_Estudiantes_estudiante_id",
                table: "Cuentas_cobrar");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_cobrar_seleccion_id",
                table: "Cuentas_cobrar");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_cobrar_estudiante_id",
                table: "Cuentas_cobrar");

            //factura servicio
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Servicio_Estudiantes_estudiante_id",
                table: "Factura_Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Servicio_Metodo_pago_metodo_pago_id",
                table: "Factura_Servicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Servicio_Servicios_servicio_id",
                table: "Factura_Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Factura_Servicio_servicio_id",
                table: "Factura_Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Factura_Servicio_estudiante_id",
                table: "Factura_Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Factura_Servicio_metodo_pago_id",
                table: "Factura_Servicio");

            //asignatura seleccion
            migrationBuilder.DropForeignKey(
                name: "FK_Asignatura_seleccion_Seleccions_seleccion_id",
                table: "Asignatura_seleccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignatura_seleccion_Asignaturas_asignatura_id",
                table: "Asignatura_seleccion");

            migrationBuilder.DropIndex(
                name: "IX_Asignatura_seleccion_seleccion_id",
                table: "Asignatura_seleccion");

            migrationBuilder.DropIndex(
                name: "IX_Asignatura_seleccion_asignatura_id",
                table: "Asignatura_seleccion");

            //pensum
            migrationBuilder.DropForeignKey(
               name: "FK_Pensum_Carreras_carrera_id",
               table: "Pensum");

            migrationBuilder.DropForeignKey(
                name: "FK_Pensum_Asignaturas_asignatura_id",
                table: "Pensum");

            migrationBuilder.DropIndex(
                name: "IX_Pensum_carrera_id",
                table: "Pensum");

            migrationBuilder.DropIndex(
                name: "IX_Pensum_asignatura_id",
                table: "Pensum");
        }
    }
}
