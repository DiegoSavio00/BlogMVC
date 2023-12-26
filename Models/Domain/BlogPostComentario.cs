namespace BlogMVC.Models.Domain
{
    public class BlogPostComentario
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime AdicionarData { get; set; }
    }
}
