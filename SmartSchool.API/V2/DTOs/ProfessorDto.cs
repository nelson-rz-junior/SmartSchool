using System;
using System.Collections.Generic;

namespace SmartSchool.API.V2.DTOs
{
    public class ProfessorDto
    {
        public int Id { get; set; }

        public int Registro { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataInicioRegistro { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }
    }
}