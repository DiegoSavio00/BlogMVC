using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Senha deve ser no minimo 6 caracteres")]
        public string Senha { get; set; }
        public string RetornoUrl { get; set; }
    }
}
