using BlogMVC.Models.Domain;
using BlogMVC.Models.ViewModels;
using BlogMVC.Repositories.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepositorio _repositorio;

        public AdminTagsController(ITagRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Adicionar() => View();

        [HttpPost]
        [ActionName("Adicionar")]
        public async Task<IActionResult> Adicionar(RequisicaoAdicionarTag addRequisicao)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var tag = new Tag
            {
                Nome = addRequisicao.Nome,
                MostrarNome = addRequisicao.MostrarNome,
            };
            await _repositorio.AdicionarAsync(tag);
            return RedirectToAction("Listar");
        }

        [HttpGet]
        [ActionName("Listar")]
        public async Task<IActionResult> Listar()
        {
            var tags = await _repositorio.BuscarTodasTagsAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var tag = await _repositorio.BuscarAsync(id);
            if (tag is not null)
            {
                var requisicaoEditar = new RequisicaoEditarTag
                {
                    Id = tag.Id,
                    Nome = tag.Nome,
                    MostrarNome = tag.MostrarNome,
                };
                return View(requisicaoEditar);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(RequisicaoEditarTag requisicaoEditar)
        {
            var tag = new Tag
            {
                Id = requisicaoEditar.Id,
                Nome = requisicaoEditar.Nome,
                MostrarNome = requisicaoEditar.MostrarNome,
            };
            var tagExistente = await _repositorio.AtualizarAsync(tag);
            return RedirectToAction("Editar", new { id = requisicaoEditar.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(RequisicaoEditarTag requisicaoEditar)
        {
            var tagDeletada = await _repositorio.DeletarAsync(requisicaoEditar.Id);
            if (tagDeletada is not null)
            {
                return RedirectToAction("Listar");
            }
            return RedirectToAction("Editar", new { id = requisicaoEditar.Id });
        }

    }
}
