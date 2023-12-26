namespace BlogMVC.Models.ViewModels
{
    public class AdicionarLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
