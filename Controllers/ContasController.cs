using BlogMVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class ContasController : Controller
    {
        private readonly UserManager<IdentityUser> _gerenciadorUsuario;
        private readonly SignInManager<IdentityUser> __signInManager;

        public ContasController(UserManager<IdentityUser> gerenciadorUsuario,
            SignInManager<IdentityUser> signInManager)
        {
            _gerenciadorUsuario = gerenciadorUsuario;
            __signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel registrarViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registrarViewModel.NomeUsuario,
                    Email = registrarViewModel.Email,
                };
                var identityResult = await _gerenciadorUsuario.CreateAsync(identityUser, registrarViewModel.Senha);
                if (identityResult.Succeeded)
                {
                    var roleIdentityResult = await _gerenciadorUsuario.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Registrar");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string RetornoUrl)
        {
            var model = new LoginViewModel
            {
                RetornoUrl = RetornoUrl,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signResult = await __signInManager.PasswordSignInAsync(loginViewModel.NomeUsuario,
                loginViewModel.Senha, false, false);
            if (signResult is not null && signResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(loginViewModel.RetornoUrl))
                {
                    return RedirectToPage(loginViewModel.RetornoUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Deslogar()
        {
            await __signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AcessoNegado() => View();

    }
}
