using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DadosUniversitarios.Data.Migrations
{
    /// <inheritdoc />
    public partial class SextaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_Cursos_CursoId",
                table: "Disciplinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_Professores_ProfessorId",
                table: "Disciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Disciplinas");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaId",
                table: "Professores",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CursoDisciplina",
                columns: table => new
                {
                    CursosId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoDisciplina", x => new { x.CursosId, x.DisciplinasId });
                    table.ForeignKey(
                        name: "FK_CursoDisciplina_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoDisciplina_Disciplinas_DisciplinasId",
                        column: x => x.DisciplinasId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professores_DisciplinaId",
                table: "Professores",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoDisciplina_DisciplinasId",
                table: "CursoDisciplina",
                column: "DisciplinasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinaId",
                table: "Professores",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinaId",
                table: "Professores");

            migrationBuilder.DropTable(
                name: "CursoDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_Professores_DisciplinaId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "DisciplinaId",
                table: "Professores");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Disciplinas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Disciplinas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplinas_Cursos_CursoId",
                table: "Disciplinas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplinas_Professores_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
