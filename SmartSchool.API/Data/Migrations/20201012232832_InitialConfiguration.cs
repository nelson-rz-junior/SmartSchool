using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace SmartSchool.API.Data.Migrations
{
    public partial class InitialConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    DataInicioMatricula = table.Column<DateTime>(nullable: false),
                    DataFimMatricula = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Registro = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataInicioRegistro = table.Column<DateTime>(nullable: false),
                    DataFimRegistro = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosCursos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosCursos", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CargaHoraria = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    PreRequisitoId = table.Column<int>(nullable: true),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Disciplinas_PreRequisitoId",
                        column: x => x.PreRequisitoId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunosDisciplinas",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    Nota = table.Column<int>(nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDisciplinas", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 173, DateTimeKind.Local).AddTicks(4793), new DateTime(2000, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "+551122223333" },
                    { 2, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1451), new DateTime(2000, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Isabela", "+551133334444" },
                    { 3, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1512), new DateTime(2002, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antonia", "+551144445555" },
                    { 4, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1518), new DateTime(1998, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Luiza", "Maria", "+551155556666" },
                    { 5, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1522), new DateTime(2001, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Machado", "+551166667777" },
                    { 6, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1529), new DateTime(2003, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedro", "Alvares", "+551177778888" },
                    { 7, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 174, DateTimeKind.Local).AddTicks(1533), new DateTime(2005, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Paulo", "José", "+551188889999" }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação" },
                    { 2, "Sistemas de Informação" },
                    { 3, "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(943), "Lauro", 1, "Oliveira", null },
                    { 2, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(1990), "Roberto", 2, "Soares", null },
                    { 3, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(2028), "Ronaldo", 3, "Marconi", null },
                    { 4, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(2030), "Rodrigo", 4, "Carvalho", null },
                    { 5, true, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(2031), "Alexandre", 5, "Montanha", null }
                });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8096) },
                    { 7, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8602) },
                    { 6, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8601) },
                    { 5, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8599) },
                    { 4, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8598) },
                    { 3, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8597) },
                    { 2, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8596) },
                    { 7, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8594) },
                    { 6, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8593) },
                    { 5, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8592) },
                    { 1, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8595) },
                    { 3, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8589) },
                    { 4, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8590) },
                    { 3, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8581) },
                    { 4, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8583) },
                    { 5, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8584) },
                    { 2, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8563) },
                    { 7, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8586) },
                    { 1, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8587) },
                    { 2, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8588) },
                    { 6, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(8585) }
                });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 9, 420, 2, "Programação", null, 5 },
                    { 1, 200, 1, "Matemática", null, 1 },
                    { 2, 400, 3, "Matemática", null, 1 },
                    { 4, 100, 1, "Português", null, 3 },
                    { 5, 150, 1, "Inglês", null, 4 },
                    { 6, 150, 2, "Inglês", null, 4 },
                    { 7, 250, 3, "Inglês", null, 4 },
                    { 8, 480, 1, "Programação", null, 5 },
                    { 10, 420, 3, "Programação", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7150), null },
                    { 4, 5, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7164), null },
                    { 2, 5, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7155), null },
                    { 1, 5, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7149), null },
                    { 7, 4, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7177), null },
                    { 6, 4, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7172), null },
                    { 5, 4, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7165), null },
                    { 4, 4, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7162), null },
                    { 1, 4, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7131), null },
                    { 5, 5, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7166), null },
                    { 7, 5, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7178), null },
                    { 6, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7168), null },
                    { 3, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7157), null },
                    { 2, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7152), null },
                    { 1, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(6636), null },
                    { 7, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7173), null },
                    { 6, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7167), null },
                    { 4, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7161), null },
                    { 3, 1, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7156), null },
                    { 7, 2, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7174), null }
                });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 3, 450, 3, "Física", 2, 2 });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 3, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7158), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 6, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7170), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 3, null, new DateTime(2020, 10, 12, 20, 28, 32, 175, DateTimeKind.Local).AddTicks(7175), null });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosCursos_CursoId",
                table: "AlunosCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunosDisciplinas_DisciplinaId",
                table: "AlunosDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_PreRequisitoId",
                table: "Disciplinas",
                column: "PreRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosCursos");

            migrationBuilder.DropTable(
                name: "AlunosDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
