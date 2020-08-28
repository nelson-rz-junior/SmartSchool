using System;

namespace SmartSchool.API.DTOs
{
    public class ProfessorRegistrarDto
    {
        public int Id { get; set; }

        public int Registro { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataInicioRegistro { get; set; }

        public DateTime? DataFimRegistro { get; set; }

        public bool Ativo { get; set; }
    }
}