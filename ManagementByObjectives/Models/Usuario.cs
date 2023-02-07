using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementByObjectives.Models
{
    public class Usuario
    {                          
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string? Nome { get; set; }
        
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }
        public int Status { get; set; }
        
        public string? Senha { get; set; }        
        public DateTime? DataCriacao { get; set; }
    }
}
