using System.ComponentModel.DataAnnotations;

namespace OperaçãoCuriosidadeApi.Dtos
{
    public record LoginDto
    {
        public string? Senha { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

    }
}
