using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OperaçãoCuriosidadeApi.Models;


public class Colaborador 
    {
        [Key]
        public int Id { get; set; }
        public bool Ativo { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(150)]
        public string? Email { get; set; }
        [Required]
        [MaxLength(150)]
    public string? Endereco { get; set; }
        [Required]
        public string? Idade { get; set; }
        [MaxLength(300)]
        public string? Interesses { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Nome { get; set; }
        [MaxLength(300)]
        public string? OutrasInfo { get; set; }
        [MaxLength(300)]
        public string? Sentimentos { get; set; }
        [MaxLength(300)]
        public string? Valores { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }





