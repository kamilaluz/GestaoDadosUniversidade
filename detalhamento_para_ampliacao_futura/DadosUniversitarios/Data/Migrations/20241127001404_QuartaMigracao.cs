using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuartaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Cursos_CursoId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_CursoId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Professores");

            migrationBuilder.CreateTable(
                name: "CursoProfessor",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    ProfessoresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfessor", x => new { x.CursoId, x.ProfessoresId });
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfessor_Professores_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfessor_ProfessoresId",
                table: "CursoProfessor",
                column: "ProfessoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoProfessor");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Professores",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_CursoId",
                table: "Professores",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Cursos_CursoId",
                table: "Professores",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}
