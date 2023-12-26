using BlogMVC.Models.Domain;

namespace BlogMVC.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public List<Usuario> Usuarios { get; set; }
        public string UsuarioNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool AdminRoleCheckBox { get; set; }

    }
}
