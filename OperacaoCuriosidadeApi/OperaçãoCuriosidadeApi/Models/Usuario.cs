using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OperaçãoCuriosidadeApi.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(150)]
    public string? NomeUsuario { get; set; }
    [Required]
    [MaxLength(150)]
    public string? Senha { get; set; }
    [EmailAddress]
    [Required]
    [MaxLength(300)]
    public string? Email { get; set; }
    public bool Admin { get; set; }
    public ICollection<Colaborador>? Colaboradores { get; set; }
    public Usuario()
    {
        Colaboradores = new Collection<Colaborador>();
    }
}

