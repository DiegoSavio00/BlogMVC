using BlogMVC.Infra;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Repositories.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AuthDbContext _authContexto;

        public UsuarioRepositorio(AuthDbContext authContexto)
        {
            _authContexto = authContexto;
        }

        public async Task<IEnumerable<IdentityUser>> BuscarTodosAsync()
        {
            var usuarios = await _authContexto.Users.ToListAsync();
            var superAdminUsuario = await _authContexto.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");
            if (superAdminUsuario is not null)
            {
                usuarios.Remove(superAdminUsuario);
            }
            return usuarios;
        }
    }
}
