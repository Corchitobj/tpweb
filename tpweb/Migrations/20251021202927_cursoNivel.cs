using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpweb.Migrations
{
    /// <inheritdoc />
    public partial class cursoNivel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Cursos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nivel", "Nombre" },
                values: new object[] { 1, "Primer Año" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nivel", "Nombre" },
                values: new object[] { 2, "Segundo Año" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Nivel", "Nombre" },
                values: new object[] { 3, "Tercer Año" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Nivel", "Nombre" },
                values: new object[] { 4, "Cuarto Año" });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Nivel", "Nombre" },
                values: new object[] { 5, "Quinto Año" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Cursos");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Primer año");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Segundo año");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Tercer año");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Nombre",
                value: "Cuarto año");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nombre",
                value: "Quinto año");
        }
    }
}
