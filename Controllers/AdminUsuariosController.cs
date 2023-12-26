using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly UserManager<IdentityUser> _gerenciadorUsuario;

        public AdminUsuariosController(IUsuarioRepositorio usuarioRepositorio,
            UserManager<IdentityUser> gerenciadorUsuario)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _gerenciadorUsuario = gerenciadorUsuario;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuarioRepositorio.BuscarTodosAsync();
            var usuariosVM = new UsuarioViewModel();
            usuariosVM.Usuarios = new List<Usuario>();
            foreach (var usuario in usuarios)
            {
                usuariosVM.Usuarios.Add(new Usuario
                {
                    Id = Guid.Parse(usuario.Id),
                    Usuarionome = usuario.UserName,
                    Email = usuario.Email
                });
            }
            return View(usuariosVM);
        }

        [HttpPost]
        public async Task<IActionResult> Listar(UsuarioViewModel requisicao)
        {
            var identityUser = new IdentityUser
            {
                UserName = requisicao.UsuarioNome,
                Email = requisicao.Email,
            };
            var identityResult = await _gerenciadorUsuario.CreateAsync(identityUser, requisicao.Senha);
            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "Usuario" };
                    if (requisicao.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }
                    identityResult = await _gerenciadorUsuario.AddToRolesAsync(identityUser, roles);
                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("Listar", "AdminUsuarios");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var usuario = await _gerenciadorUsuario.FindByIdAsync(id.ToString());
            if (usuario is not null)
            {
                var identityResult = await _gerenciadorUsuario.DeleteAsync(usuario);
                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("Listar", "AdminUsuarios");
                }
            }
            return View();
        }

    }
}
