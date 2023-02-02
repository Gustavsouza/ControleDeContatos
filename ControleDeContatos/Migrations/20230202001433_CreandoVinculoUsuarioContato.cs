using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeContatos.Migrations
{
    /// <inheritdoc />
    public partial class CreandoVinculoUsuarioContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "usuarioId",
                table: "contatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_contatos_usuarioId",
                table: "contatos",
                column: "usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_contatos_Usuarios_usuarioId",
                table: "contatos",
                column: "usuarioId",
                principalTable: "Usuarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contatos_Usuarios_usuarioId",
                table: "contatos");

            migrationBuilder.DropIndex(
                name: "IX_contatos_usuarioId",
                table: "contatos");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "contatos");
        }
    }
}
