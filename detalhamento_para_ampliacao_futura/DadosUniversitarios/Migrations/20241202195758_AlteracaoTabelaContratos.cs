using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoTabelaContratos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Periodicidade",
                table: "Contratos",
                newName: "PeriodicidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_PeriodicidadeId",
                table: "Contratos",
                column: "PeriodicidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Periodicidade_PeriodicidadeId",
                table: "Contratos",
                column: "PeriodicidadeId",
                principalTable: "Periodicidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Periodicidade_PeriodicidadeId",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_PeriodicidadeId",
                table: "Contratos");

            migrationBuilder.RenameColumn(
                name: "PeriodicidadeId",
                table: "Contratos",
                newName: "Periodicidade");
        }
    }
}
