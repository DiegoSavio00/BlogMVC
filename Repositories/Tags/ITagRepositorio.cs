using BlogMVC.Models.Domain;

namespace BlogMVC.Repositories.Tags
{
    public interface ITagRepositorio
    {
        Task<IEnumerable<Tag>> BuscarTodasTagsAsync();
        Task<Tag?> BuscarAsync(Guid id);
        Task<Tag?> AdicionarAsync(Tag tag);
        Task<Tag?> AtualizarAsync(Tag tag);
        Task<Tag?> DeletarAsync(Guid id);
    }
}
