using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public class SmartSchoolContext : DbContext
    {
        public SmartSchoolContext(DbContextOptions<SmartSchoolContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Professor> Professores { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public DbSet<AlunoCurso> AlunosCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            modelBuilder.Entity<AlunoDisciplina>()
                .HasKey(p => new { p.AlunoId, p.DisciplinaId });

            modelBuilder.Entity<AlunoCurso>()
                .HasKey(p => new { p.AlunoId, p.CursoId });

            modelBuilder.Entity<Aluno>()
                .HasData(new List<Aluno>
                {
                    new Aluno(1, 1, "Marta", "Kent", "+551122223333", DateTime.Parse("2000/03/14")),
                    new Aluno(2, 2, "Paula", "Isabela", "+551133334444", DateTime.Parse("2000/09/24")),
                    new Aluno(3, 3, "Laura", "Antonia", "+551144445555", DateTime.Parse("2002/06/10")),
                    new Aluno(4, 4, "Luiza", "Maria", "+551155556666", DateTime.Parse("1998/04/30")),
                    new Aluno(5, 5, "Lucas", "Machado", "+551166667777", DateTime.Parse("2001/02/15")),
                    new Aluno(6, 6, "Pedro", "Alvares", "+551177778888", DateTime.Parse("2003/10/12")),
                    new Aluno(7, 7, "Paulo", "José", "+551188889999", DateTime.Parse("2005/12/02"))
                });

            modelBuilder.Entity<Professor>()
                .HasData(new List<Professor>
                {
                    new Professor(1, 1, "Lauro", "Oliveira"),
                    new Professor(2, 2, "Roberto", "Soares"),
                    new Professor(3, 3, "Ronaldo", "Marconi"),
                    new Professor(4, 4, "Rodrigo", "Carvalho"),
                    new Professor(5, 5, "Alexandre", "Montanha"),
                });

            modelBuilder.Entity<Curso>()
                .HasData(new List<Curso>
                {
                    new Curso(1, "Tecnologia da Informação"),
                    new Curso(2, "Sistemas de Informação"),
                    new Curso(3, "Ciência da Computação")             
                });
            
            modelBuilder.Entity<Disciplina>()
                .HasData(new List<Disciplina>
                {
                    new Disciplina { Id = 1, Nome = "Matemática", ProfessorId = 1, CursoId = 1, CargaHoraria = 200 },
                    new Disciplina { Id = 2, Nome = "Matemática", ProfessorId = 1, CursoId = 3, CargaHoraria = 400 },
                    new Disciplina { Id = 3, Nome = "Física", ProfessorId = 2, CursoId = 3, CargaHoraria = 450, PreRequisitoId = 2 },
                    new Disciplina { Id = 4, Nome = "Português", ProfessorId = 3, CursoId = 1, CargaHoraria = 100 },
                    new Disciplina { Id = 5, Nome = "Inglês", ProfessorId = 4, CursoId = 1, CargaHoraria = 150 },
                    new Disciplina { Id = 6, Nome = "Inglês", ProfessorId = 4, CursoId = 2, CargaHoraria = 150 },
                    new Disciplina { Id = 7, Nome = "Inglês", ProfessorId = 4, CursoId = 3, CargaHoraria = 250 },
                    new Disciplina { Id = 8, Nome = "Programação", ProfessorId = 5, CursoId = 1, CargaHoraria = 480 },
                    new Disciplina { Id = 9, Nome = "Programação", ProfessorId = 5, CursoId = 2, CargaHoraria = 420 },
                    new Disciplina { Id = 10, Nome = "Programação", ProfessorId = 5, CursoId = 3, CargaHoraria = 420 }
                });
            
            modelBuilder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina> 
                {
                    new AlunoDisciplina { AlunoId = 1, DisciplinaId = 2 },
                    new AlunoDisciplina { AlunoId = 1, DisciplinaId = 4 },
                    new AlunoDisciplina { AlunoId = 1, DisciplinaId = 5 },
                    new AlunoDisciplina { AlunoId = 2, DisciplinaId = 1 },
                    new AlunoDisciplina { AlunoId = 2, DisciplinaId = 2 },
                    new AlunoDisciplina { AlunoId = 2, DisciplinaId = 5 },
                    new AlunoDisciplina { AlunoId = 3, DisciplinaId = 1 },
                    new AlunoDisciplina { AlunoId = 3, DisciplinaId = 2 },
                    new AlunoDisciplina { AlunoId = 3, DisciplinaId = 3 },
                    new AlunoDisciplina { AlunoId = 4, DisciplinaId = 1 },
                    new AlunoDisciplina { AlunoId = 4, DisciplinaId = 4 },
                    new AlunoDisciplina { AlunoId = 4, DisciplinaId = 5 },
                    new AlunoDisciplina { AlunoId = 5, DisciplinaId = 4 },
                    new AlunoDisciplina { AlunoId = 5, DisciplinaId = 5 },
                    new AlunoDisciplina { AlunoId = 6, DisciplinaId = 1 },
                    new AlunoDisciplina { AlunoId = 6, DisciplinaId = 2 },
                    new AlunoDisciplina { AlunoId = 6, DisciplinaId = 3 },
                    new AlunoDisciplina { AlunoId = 6, DisciplinaId = 4 },
                    new AlunoDisciplina { AlunoId = 7, DisciplinaId = 1 },
                    new AlunoDisciplina { AlunoId = 7, DisciplinaId = 2 },
                    new AlunoDisciplina { AlunoId = 7, DisciplinaId = 3 },
                    new AlunoDisciplina { AlunoId = 7, DisciplinaId = 4 },
                    new AlunoDisciplina { AlunoId = 7, DisciplinaId = 5 }
                });

            modelBuilder.Entity<AlunoCurso>()
                .HasData(new AlunoCurso[]
                {
                    new AlunoCurso { AlunoId = 1, CursoId = 1 },
                    new AlunoCurso { AlunoId = 2, CursoId = 1 },
                    new AlunoCurso { AlunoId = 3, CursoId = 1 },
                    new AlunoCurso { AlunoId = 4, CursoId = 1 },
                    new AlunoCurso { AlunoId = 5, CursoId = 1 },
                    new AlunoCurso { AlunoId = 6, CursoId = 1 },
                    new AlunoCurso { AlunoId = 7, CursoId = 1 },
                    new AlunoCurso { AlunoId = 1, CursoId = 2 },
                    new AlunoCurso { AlunoId = 2, CursoId = 2 },
                    new AlunoCurso { AlunoId = 3, CursoId = 2 },
                    new AlunoCurso { AlunoId = 4, CursoId = 2 },
                    new AlunoCurso { AlunoId = 5, CursoId = 2 },
                    new AlunoCurso { AlunoId = 6, CursoId = 2 },
                    new AlunoCurso { AlunoId = 7, CursoId = 2 },
                    new AlunoCurso { AlunoId = 1, CursoId = 3 },
                    new AlunoCurso { AlunoId = 2, CursoId = 3 },
                    new AlunoCurso { AlunoId = 3, CursoId = 3 },
                    new AlunoCurso { AlunoId = 4, CursoId = 3 },
                    new AlunoCurso { AlunoId = 5, CursoId = 3 },
                    new AlunoCurso { AlunoId = 6, CursoId = 3 },
                    new AlunoCurso { AlunoId = 7, CursoId = 3 }
                });
        }
    }
}