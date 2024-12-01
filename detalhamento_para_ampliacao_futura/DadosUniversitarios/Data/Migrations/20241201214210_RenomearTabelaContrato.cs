using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenomearTabelaContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fornecedores",
                table: "Fornecedores");

            migrationBuilder.RenameTable(
                name: "Fornecedores",
                newName: "Contratos");

            migrationBuilder.RenameIndex(
                name: "IX_Fornecedores_EmpresaId",
                table: "Contratos",
                newName: "IX_Contratos_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Empresas_EmpresaId",
                table: "Contratos",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Empresas_EmpresaId",
                table: "Contratos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contratos",
                table: "Contratos");

            migrationBuilder.RenameTable(
                name: "Contratos",
                newName: "Fornecedores");

            migrationBuilder.RenameIndex(
                name: "IX_Contratos_EmpresaId",
                table: "Fornecedores",
                newName: "IX_Fornecedores_EmpresaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fornecedores",
                table: "Fornecedores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Empresas_EmpresaId",
                table: "Fornecedores",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
