namespace BlogMVC.Models.Domain
{
    public class BlogPostLike
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
