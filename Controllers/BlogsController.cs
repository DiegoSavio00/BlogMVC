using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.BlogPostComentarios;
using BlogMVC.Repositories.BlogPostLikes;
using BlogMVC.Repositories.BlogPosts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepositorio _repositorio;
        private readonly IBlogPostLikeRepositorio _repositorioLike;
        private readonly IBlogPostComentarioRepositorio _repositorioComentario;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostRepositorio repositorio,
            IBlogPostLikeRepositorio repositorioLike,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostComentarioRepositorio repositorioComentario)
        {
            _repositorio = repositorio;
            _repositorioLike = repositorioLike;
            _signInManager = signInManager;
            _userManager = userManager;
            _repositorioComentario = repositorioComentario;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlH)
        {
            var blogPost = await _repositorio.BuscarPorUrlAsync(urlH);
            var detalhesBlogVM = new DetalhesBlogViewModel();
            var liked = false;
            if (blogPost is not null)
            {
                var totalLikes = await _repositorioLike.BuscarTotalLikes(blogPost.Id);
                if (_signInManager.IsSignedIn(User))
                {
                    var likesDoBlog = await _repositorioLike.BuscarLikesParaBlog(blogPost.Id);
                    var usuarioId = _userManager.GetUserId(User);
                    if (usuarioId is not null)
                    {
                        var likeDoUsuario = likesDoBlog.FirstOrDefault(x => x.UsuarioId == Guid.Parse(usuarioId));
                        liked = likeDoUsuario is not null;
                    }
                }
                var blogComentarioDM = await _repositorioComentario.BuscarTodosComentariosDoBlogPeloId(blogPost.Id);
                var blogComentarioFM = new List<BlogComentario>();
                foreach (var blogComentario in blogComentarioDM)
                {
                    blogComentarioFM.Add(new BlogComentario
                    {
                        Descricao = blogComentario.Descricao,
                        AdicionarData = blogComentario.AdicionarData,
                        Usuarionome = (await _userManager.FindByIdAsync(blogComentario.UsuarioId.ToString())).UserName,
                    });
                }
                detalhesBlogVM = new DetalhesBlogViewModel
                {
                    Id = blogPost.Id,
                    Conteudo = blogPost.Conteudo,
                    TituloPagina = blogPost.TituloPagina,
                    Autor = blogPost.Autor,
                    ImageUrl = blogPost.ImageUrl,
                    Cabecalho = blogPost.Cabecalho,
                    DataPublicacao = blogPost.DataPublicacao,
                    UrlH = blogPost.UrlH,
                    Visivel = blogPost.Visivel,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    DescricaoCurta = blogPost.DescricaoCurta,
                    Liked = liked,
                    Comentarios = blogComentarioFM,
                };
            }
            return View(detalhesBlogVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DetalhesBlogViewModel detalhesBlogViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComentario
                {
                    BlogPostId = detalhesBlogViewModel.Id,
                    Descricao = detalhesBlogViewModel.DescricaoComentario,
                    UsuarioId = Guid.Parse(_userManager.GetUserId(User)),
                    AdicionarData = DateTime.Now,
                };
                await _repositorioComentario.AdicionarAsync(domainModel);
                return RedirectToAction("Index", "Blogs", new { urlH = detalhesBlogViewModel.UrlH });
            }
            return View();
        }

    }
}
