using System;

namespace SmartSchool.API.V1.DTOs
{
    public class AlunoRegistrarDto
    {
        public int Id { get; set; }

        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInicioMatricula { get; set; }

        public DateTime? DataFimMatricula { get; set; }

        public bool Ativo { get; set; }
    }
}