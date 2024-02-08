using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertarDataProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Productos Values ('Mouse',20.00, 1 , 32, 'Uso para estilo gamer'), ('Teclado',45.00, 0 , 14, 'Uso para estilo oficinista') ");
                                                                
        }
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Productos");
        }
    }
}
