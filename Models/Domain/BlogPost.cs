namespace BlogMVC.Models.Domain
{
    public class BlogPost
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
        public ICollection<BlogPostLike> Likes { get; set; }
        public ICollection<BlogPostComentario> Comentarios { get; set; }
    }
}
