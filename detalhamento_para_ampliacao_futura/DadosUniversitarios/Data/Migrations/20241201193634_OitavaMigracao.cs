using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class OitavaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinaId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_DisciplinaId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "DisciplinaId",
                table: "Professores");

            migrationBuilder.CreateTable(
                name: "DisciplinaProfessor",
                columns: table => new
                {
                    DisciplinasId = table.Column<int>(type: "integer", nullable: false),
                    ProfessoresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaProfessor", x => new { x.DisciplinasId, x.ProfessoresId });
                    table.ForeignKey(
                        name: "FK_DisciplinaProfessor_Disciplinas_DisciplinasId",
                        column: x => x.DisciplinasId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaProfessor_Professores_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaProfessor_ProfessoresId",
                table: "DisciplinaProfessor",
                column: "ProfessoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinaProfessor");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaId",
                table: "Professores",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_DisciplinaId",
                table: "Professores",
                column: "DisciplinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinaId",
                table: "Professores",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id");
        }
    }
}
