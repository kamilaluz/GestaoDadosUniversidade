using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuintaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPagamento",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "NumeroContrato",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Periodicidade",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "ValorServico",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "VencimentoContrato",
                table: "Empresas");

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    NumeroContrato = table.Column<int>(type: "integer", nullable: false),
                    Periodicidade = table.Column<int>(type: "integer", nullable: false),
                    ValorServico = table.Column<double>(type: "double precision", nullable: false),
                    DataPagamento = table.Column<DateOnly>(type: "date", nullable: false),
                    VencimentoContrato = table.Column<DateOnly>(type: "date", nullable: false),
                    EmpresaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedores_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId",
                table: "Fornecedores",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataPagamento",
                table: "Empresas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Empresas",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroContrato",
                table: "Empresas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Periodicidade",
                table: "Empresas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorServico",
                table: "Empresas",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "VencimentoContrato",
                table: "Empresas",
                type: "date",
                nullable: true);
        }
    }
}
