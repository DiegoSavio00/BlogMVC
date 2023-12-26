using BlogMVC.Repositories.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepositorio _repositorio;

        public ImagesController(IImageRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _repositorio.UploadAsync(file);
            if (imageUrl is not null)
            {
                return Problem("Alguma está errada!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl });
        }
    }
}
