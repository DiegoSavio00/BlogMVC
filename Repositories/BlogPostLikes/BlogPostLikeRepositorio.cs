
using BlogMVC.Infra;
using BlogMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Repositories.BlogPostLikes
{
    public class BlogPostLikeRepositorio : IBlogPostLikeRepositorio
    {
        private readonly BlogDbContext _contexto;

        public BlogPostLikeRepositorio(BlogDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<BlogPostLike> AdicionarLikeParaBlog(BlogPostLike blogPostLike)
        {
            await _contexto.Likes.AddAsync(blogPostLike);
            await _contexto.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> BuscarLikesParaBlog(Guid blogPostId)
        {
            return await _contexto.Likes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> BuscarTotalLikes(Guid blogPostId)
        {
            return await _contexto.Likes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
