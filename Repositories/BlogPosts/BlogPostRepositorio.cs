using BlogMVC.Infra;
using BlogMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Repositories.BlogPosts
{
    public class BlogPostRepositorio : IBlogPostRepositorio
    {
        private readonly BlogDbContext _contexto;

        public BlogPostRepositorio(BlogDbContext repositorio)
        {
            _contexto = repositorio;
        }

        public async Task<BlogPost?> AdicionarAsync(BlogPost post)
        {
            await _contexto.AddAsync(post);
            await _contexto.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> AtualizarAsync(BlogPost post)
        {
            var blogExistente = await _contexto.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == post.Id);
            if (blogExistente is not null)
            {
                blogExistente.Id = post.Id;
                blogExistente.Cabecalho = post.Cabecalho;
                blogExistente.TituloPagina = post.TituloPagina;
                blogExistente.Conteudo = post.Conteudo;
                blogExistente.DescricaoCurta = post.DescricaoCurta;
                blogExistente.Autor = post.Autor;
                blogExistente.DataPublicacao = post.DataPublicacao;
                blogExistente.UrlH = post.UrlH;
                blogExistente.ImageUrl = post.ImageUrl;
                blogExistente.Tags = post.Tags;
                blogExistente.Visivel = post.Visivel;
                await _contexto.SaveChangesAsync();
            }
            return null;
        }

        public async Task<BlogPost?> BuscarAsync(Guid id)
        {
            return await _contexto.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> BuscarPorUrlAsync(string urlH)
        {
            return await _contexto.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlH == urlH);
        }

        public async Task<IEnumerable<BlogPost>> BuscarTodosPostAsync()
        {
            return await _contexto.BlogPosts
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<BlogPost?> DeletarAsync(Guid id)
        {
            var tagExistente = await _contexto.BlogPosts.FindAsync(id);
            if (tagExistente is not null)
            {
                _contexto.BlogPosts.Remove(tagExistente);
                await _contexto.SaveChangesAsync();
            }
            return null;
        }
    }
}
