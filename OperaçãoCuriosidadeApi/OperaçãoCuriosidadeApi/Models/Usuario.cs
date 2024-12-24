using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OperaçãoCuriosidadeApi.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? NomeUsuario { get; set; }
    [Required]
    public string? Senha { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public bool Admin { get; set; }
    public ICollection<Colaborador>? Colaboradores { get; set; }
    public Usuario()
    {
        Colaboradores = new Collection<Colaborador>();
    }
}

