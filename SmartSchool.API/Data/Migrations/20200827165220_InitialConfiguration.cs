using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                values: new object[] { 1, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 628, DateTimeKind.Local).AddTicks(2923), new DateTime(2000, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "+551122223333" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 2, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3289), new DateTime(2000, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Isabela", "+551133334444" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 3, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3362), new DateTime(2002, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antonia", "+551144445555" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 4, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3374), new DateTime(1998, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Luiza", "Maria", "+551155556666" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 5, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3379), new DateTime(2001, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Machado", "+551166667777" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 6, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3390), new DateTime(2003, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedro", "Alvares", "+551177778888" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFimMatricula", "DataInicioMatricula", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 7, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 629, DateTimeKind.Local).AddTicks(3395), new DateTime(2005, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Paulo", "José", "+551188889999" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Tecnologia da Informação" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Sistemas de Informação" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 3, "Ciência da Computação" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[] { 1, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 630, DateTimeKind.Local).AddTicks(5888), "Lauro", 1, "Oliveira", null });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[] { 2, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 630, DateTimeKind.Local).AddTicks(7180), "Roberto", 2, "Soares", null });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[] { 3, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 630, DateTimeKind.Local).AddTicks(7228), "Ronaldo", 3, "Marconi", null });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[] { 4, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 630, DateTimeKind.Local).AddTicks(7230), "Rodrigo", 4, "Carvalho", null });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFimRegistro", "DataInicioRegistro", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[] { 5, true, null, new DateTime(2020, 8, 27, 13, 52, 19, 630, DateTimeKind.Local).AddTicks(7232), "Alexandre", 5, "Montanha", null });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 1, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5145) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 7, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5757) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 6, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5756) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 5, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5754) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 4, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5753) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 3, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5752) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 2, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5750) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 7, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5747) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 6, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5746) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 5, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5745) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 1, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5749) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 3, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5742) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 4, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5743) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 3, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5731) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 4, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5733) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 5, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5735) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 2, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5709) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 7, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5738) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 1, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5739) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 2, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5740) });

            migrationBuilder.InsertData(
                table: "AlunosCursos",
                columns: new[] { "AlunoId", "CursoId", "DataFim", "DataInicio" },
                values: new object[] { 6, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(5736) });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 9, 420, 2, "Programação", null, 5 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 1, 200, 1, "Matemática", null, 1 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 2, 400, 3, "Matemática", null, 1 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 4, 100, 1, "Português", null, 3 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 5, 150, 1, "Inglês", null, 4 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 6, 150, 2, "Inglês", null, 4 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 7, 250, 3, "Inglês", null, 4 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 8, 480, 1, "Programação", null, 5 });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 10, 420, 3, "Programação", null, 5 });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 2, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4076), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 4, 5, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4093), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 2, 5, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4082), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 1, 5, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4073), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 4, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4109), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 6, 4, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4103), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 5, 4, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4094), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 4, 4, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4091), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 1, 4, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4052), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 5, 5, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4096), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 5, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4111), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 6, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4099), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 3, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4085), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 2, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4078), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 1, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(3382), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4105), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 6, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4097), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 4, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4090), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 3, 1, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4083), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 2, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4106), null });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PreRequisitoId", "ProfessorId" },
                values: new object[] { 3, 450, 3, "Física", 2, 2 });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 3, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4087), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 6, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4100), null });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[] { 7, 3, null, new DateTime(2020, 8, 27, 13, 52, 19, 631, DateTimeKind.Local).AddTicks(4108), null });

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
