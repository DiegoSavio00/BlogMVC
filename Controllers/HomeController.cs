using BlogMVC.Models;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.BlogPosts;
using BlogMVC.Repositories.Tags;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepositorio _repositorio;
        private readonly ITagRepositorio _repositorioTag;

        public HomeController(ILogger<HomeController> logger,
            IBlogPostRepositorio repositorio,
            ITagRepositorio repositorioTag)
        {
            _logger = logger;
            _repositorio = repositorio;
            _repositorioTag = repositorioTag;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _repositorio.BuscarTodosPostAsync();
            var tags = await _repositorioTag.BuscarTodasTagsAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
