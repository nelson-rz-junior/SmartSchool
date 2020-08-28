using System;

namespace SmartSchool.API.DTOs
{
    public class AlunoDto
    {   
        public int Id { get; set; }

        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataInicioMatricula { get; set; }

        public bool Ativo { get; set; }
    }
}