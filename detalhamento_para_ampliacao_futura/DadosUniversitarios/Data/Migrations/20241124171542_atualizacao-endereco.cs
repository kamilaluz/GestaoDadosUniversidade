using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaoendereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Enderecos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Enderecos");
        }
    }
}
