using BlogMVC.Models.Domain;

namespace BlogMVC.Repositories.BlogPostComentarios
{
    public interface IBlogPostComentarioRepositorio
    {
        Task<BlogPostComentario> AdicionarAsync(BlogPostComentario blogPostComentario);
        Task<IEnumerable<BlogPostComentario>> BuscarTodosComentariosDoBlogPeloId(Guid blogPostId);
    }
}
