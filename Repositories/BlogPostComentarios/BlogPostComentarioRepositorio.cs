using BlogMVC.Infra;
using BlogMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Repositories.BlogPostComentarios
{
    public class BlogPostComentarioRepositorio : IBlogPostComentarioRepositorio
    {
        private readonly BlogDbContext _contexto;

        public BlogPostComentarioRepositorio(BlogDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<BlogPostComentario> AdicionarAsync(BlogPostComentario blogPostComentario)
        {
            await _contexto.Comentarios.AddAsync(blogPostComentario);
            await _contexto.SaveChangesAsync();
            return blogPostComentario;
        }

        public async Task<IEnumerable<BlogPostComentario>> BuscarTodosComentariosDoBlogPeloId(Guid blogPostId)
        {
            return await _contexto.Comentarios.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
