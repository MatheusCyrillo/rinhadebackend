using System.ComponentModel.DataAnnotations;

namespace rinha_minimal_api.Models
{
    public class Pessoa
    {
        public Guid Id { get; set; }

        public required string Apelido { get; set; }

        public required string Nome { get; set; }

        public required string Nascimento { get; set; }

        public string[]? Stack { get; set; }

    }
}
