using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Relacion_Producto_TipoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTipoProducto",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TipoProductos_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto",
                principalTable: "TipoProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TipoProductos_IdTipoProducto",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "IdTipoProducto",
                table: "Productos");
        }
    }
}
