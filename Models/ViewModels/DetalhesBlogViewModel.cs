using BlogMVC.Models.Domain;

namespace BlogMVC.Models.ViewModels
{
    public class DetalhesBlogViewModel
    {
        public Guid Id { get; set; }
        public string Cabecalho { get; set; }
        public string TituloPagina { get; set; }
        public string Conteudo { get; set; }
        public string DescricaoCurta { get; set; }
        public string ImageUrl { get; set; }
        public string UrlH { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public bool Visivel { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public string DescricaoComentario { get; set; }
        public IEnumerable<BlogComentario> Comentarios { get; set; }

    }
}
