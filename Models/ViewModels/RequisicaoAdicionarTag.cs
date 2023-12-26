using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models.ViewModels
{
    public class RequisicaoAdicionarTag
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string MostrarNome { get; set; }
    }
}
