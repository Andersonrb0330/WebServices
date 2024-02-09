using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertarDataUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into  Usuarios Values ('Anderson','Briceño', 22 , '2001-03-30', '+51 934236983'), " +
                                                              "('Brayan','Rios', 31 , '1999-01-21', '+54 546546454') ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Usuarios");
        }
    }
}
