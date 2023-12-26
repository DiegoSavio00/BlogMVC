using BlogMVC.Infra;
using BlogMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Repositories.Tags
{
    public class TagRepositorio : ITagRepositorio
    {
        private readonly BlogDbContext _contexto;

        public TagRepositorio(BlogDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Tag?> AdicionarAsync(Tag tag)
        {
            await _contexto.Tags.AddAsync(tag);
            await _contexto.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> AtualizarAsync(Tag tag)
        {
            var existeTag = await _contexto.Tags.FindAsync(tag.Id);
            if (existeTag is not null)
            {
                existeTag.Nome = tag.Nome;
                existeTag.MostrarNome = tag.MostrarNome;
                await _contexto.SaveChangesAsync();
                return existeTag;
            }
            return null;

        }

        public async Task<Tag?> BuscarAsync(Guid id)
        {
            return await _contexto.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Tag>> BuscarTodasTagsAsync()
        {
            return await _contexto.Tags.ToListAsync();
        }

        public async Task<Tag?> DeletarAsync(Guid id)
        {
            var tag = await _contexto.Tags.FindAsync(id);
            if (tag is not null)
            {
                _contexto.Tags.Remove(tag);
                await _contexto.SaveChangesAsync();
                return tag;
            }
            return null;
        }
    }
}
