using BlogMVC.Models.Domain;

namespace BlogMVC.Repositories.BlogPosts
{
    public interface IBlogPostRepositorio
    {
        Task<IEnumerable<BlogPost>> BuscarTodosPostAsync();
        Task<BlogPost?> BuscarAsync(Guid id);
        Task<BlogPost?> AdicionarAsync(BlogPost post);
        Task<BlogPost?> AtualizarAsync(BlogPost post);
        Task<BlogPost?> DeletarAsync(Guid id);
        Task<BlogPost?> BuscarPorUrlAsync(string urlH);
    }
}
