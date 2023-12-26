using BlogMVC.Models.Domain;

namespace BlogMVC.Repositories.BlogPostLikes
{
    public interface IBlogPostLikeRepositorio
    {
        Task<int> BuscarTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AdicionarLikeParaBlog(BlogPostLike blogPostLike);
        Task<IEnumerable<BlogPostLike>> BuscarLikesParaBlog(Guid blogPostId);
    }
}
