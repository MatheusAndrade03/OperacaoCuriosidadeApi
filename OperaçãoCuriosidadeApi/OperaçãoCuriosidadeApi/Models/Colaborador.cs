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
        public string? Email { get; set; }
        [Required]
        public string? Endereco { get; set; }
        [Required]
        public string? Idade { get; set; }
        public string? Interesses { get; set; }
        [Required]
        public string? Nome { get; set; }
        public string? OutrasInfo { get; set; }
        public string? Sentimentos { get; set; }
        public string? Valores { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }





