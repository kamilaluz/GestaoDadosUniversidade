using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoTabelaPeriodicidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Fornecedores",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Periodicidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodicidade", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Periodicidade",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Diária" },
                    { 2, "Semanal" },
                    { 3, "Quinzenal" },
                    { 4, "Mensal" },
                    { 5, "Bimestral" },
                    { 6, "Trimestral" },
                    { 7, "Semestral" },
                    { 8, "Anual" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Periodicidade");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Fornecedores",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
