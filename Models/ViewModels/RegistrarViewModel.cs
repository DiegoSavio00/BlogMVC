using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models.ViewModels
{
    public class RegistrarViewModel
    {
        [Required]
        public string NomeUsuario { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Senha deve ser no minimo 6 caracteres")]
        public string Senha { get; set; }
    }
}
