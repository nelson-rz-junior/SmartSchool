namespace SmartSchool.API.V2.DTOs
{
    public class AlunoPatchDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }
    }
}