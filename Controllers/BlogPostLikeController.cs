using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.BlogPostLikes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepositorio _repositorioLike;

        public BlogPostLikeController(IBlogPostLikeRepositorio repositorioLike)
        {
            _repositorioLike = repositorioLike;
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarLike([FromBody] AdicionarLikeRequest adicionarLikeRequest)
        {
            var model = new BlogPostLike
            {
                BlogPostId = adicionarLikeRequest.BlogPostId,
                UsuarioId = adicionarLikeRequest.UsuarioId,
            };
            await _repositorioLike.AdicionarLikeParaBlog(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> BuscarTotalLikesParaBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _repositorioLike.BuscarTotalLikes(blogPostId);
            return Ok(totalLikes);
        }

    }
}
