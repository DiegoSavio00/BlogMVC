using Microsoft.AspNetCore.Identity;

namespace BlogMVC.Repositories.Usuarios
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<IdentityUser>> BuscarTodosAsync();
    }
}
