using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogMVC.Models.ViewModels
{
    public class RequisicaoAdicionarBlogPost
    {
        public string Cabecalho { get; set; }
        public string TituloPagina { get; set; }
        public string Conteudo { get; set; }
        public string DescricaoCurta { get; set; }
        public string ImageUrl { get; set; }
        public string UrlH { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public bool Visivel { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] TagSelecionada { get; set; } = Array.Empty<string>();
    }
}
