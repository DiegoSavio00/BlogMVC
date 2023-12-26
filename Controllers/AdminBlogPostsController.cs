using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.BlogPosts;
using BlogMVC.Repositories.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepositorio _repositorio;
        private readonly IBlogPostRepositorio _repositorioBlogPost;

        public AdminBlogPostsController(ITagRepositorio repositorio, IBlogPostRepositorio repositorioBlogPost)
        {
            _repositorio = repositorio;
            _repositorioBlogPost = repositorioBlogPost;
        }

        [HttpGet]
        public async Task<IActionResult> Adicionar()
        {
            var tags = await _repositorio.BuscarTodasTagsAsync();
            var model = new RequisicaoAdicionarBlogPost
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(RequisicaoAdicionarBlogPost requisicaoAdicionarPost)
        {
            var blogPost = new BlogPost
            {
                Cabecalho = requisicaoAdicionarPost.Cabecalho,
                TituloPagina = requisicaoAdicionarPost.TituloPagina,
                Conteudo = requisicaoAdicionarPost.Conteudo,
                DescricaoCurta = requisicaoAdicionarPost.DescricaoCurta,
                ImageUrl = requisicaoAdicionarPost.ImageUrl,
                UrlH = requisicaoAdicionarPost.UrlH,
                DataPublicacao = requisicaoAdicionarPost.DataPublicacao,
                Autor = requisicaoAdicionarPost.Autor,
                Visivel = requisicaoAdicionarPost.Visivel,
            };
            var tagsSelecionadas = new List<Tag>();
            foreach (var tagSelecionadaId in requisicaoAdicionarPost.TagSelecionada)
            {
                var tagsSelecionadaIdGuid = Guid.Parse(tagSelecionadaId);
                var tagExistente = await _repositorio.BuscarAsync(tagsSelecionadaIdGuid);
                if (tagExistente is not null)
                {
                    tagsSelecionadas.Add(tagExistente);
                }
            }
            blogPost.Tags = tagsSelecionadas;
            await _repositorioBlogPost.AdicionarAsync(blogPost);
            return RedirectToAction("Adicionar");
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var blogPost = await _repositorioBlogPost.BuscarTodosPostAsync();
            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var blogPost = await _repositorioBlogPost.BuscarAsync(id);
            var tagsDM = await _repositorio.BuscarTodasTagsAsync();
            if (blogPost is not null)
            {
                var model = new RequisicaoEditarBlogPost
                {
                    Id = blogPost.Id,
                    Cabecalho = blogPost.Cabecalho,
                    TituloPagina = blogPost.TituloPagina,
                    Conteudo = blogPost.Conteudo,
                    Autor = blogPost.Autor,
                    ImageUrl = blogPost.ImageUrl,
                    UrlH = blogPost.UrlH,
                    DescricaoCurta = blogPost.DescricaoCurta,
                    DataPublicacao = blogPost.DataPublicacao,
                    Visivel = blogPost.Visivel,
                    Tags = tagsDM.Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() }),
                    TagSelecionada = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(RequisicaoEditarBlogPost requisicaoEditarBlogPost)
        {
            var blogPostDM = new BlogPost
            {
                Id = requisicaoEditarBlogPost.Id,
                Cabecalho = requisicaoEditarBlogPost.Cabecalho,
                TituloPagina = requisicaoEditarBlogPost.TituloPagina,
                Conteudo = requisicaoEditarBlogPost.Conteudo,
                Autor = requisicaoEditarBlogPost.Autor,
                DescricaoCurta = requisicaoEditarBlogPost.DescricaoCurta,
                ImageUrl = requisicaoEditarBlogPost.ImageUrl,
                UrlH = requisicaoEditarBlogPost.UrlH,
                DataPublicacao = requisicaoEditarBlogPost.DataPublicacao,
                Visivel = requisicaoEditarBlogPost.Visivel,
            };
            var tagsSelecionadas = new List<Tag>();
            foreach (var tagsSelecionada in requisicaoEditarBlogPost.TagSelecionada)
            {
                if (Guid.TryParse(tagsSelecionada, out var tag))
                {
                    var tagEncontrada = await _repositorio.BuscarAsync(tag);
                    if (tagEncontrada is not null)
                    {
                        tagsSelecionadas.Add(tagEncontrada);
                    }
                }
            }
            blogPostDM.Tags = tagsSelecionadas;
            var atualizarBlogPost = await _repositorioBlogPost.AtualizarAsync(blogPostDM);
            if (atualizarBlogPost is not null)
            {
                return RedirectToAction("Editar");
            }
            return RedirectToAction("Editar");
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(RequisicaoEditarBlogPost requisicaoBlogPost)
        {
            var blogPostDeletado = await _repositorioBlogPost.DeletarAsync(requisicaoBlogPost.Id);
            if (blogPostDeletado is not null)
            {
                return RedirectToAction("Listar");
            }
            return RedirectToAction("Editar", new { id = requisicaoBlogPost.Id });
        }

    }
}
