using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Aluno
    {   
        public int Id { get; set; }

        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInicioMatricula { get; set; } = DateTime.Now;

        public DateTime? DataFimMatricula { get; set; } = null;

        public bool Ativo { get; set; } = true;

        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public Aluno()
        {
        }

        public Aluno(int id, int matricula, string nome, string sobrenome, string telefone, DateTime dataNascimento)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Telefone = telefone;
            this.Matricula = matricula;
            this.DataNascimento = dataNascimento;
        }
    }
}