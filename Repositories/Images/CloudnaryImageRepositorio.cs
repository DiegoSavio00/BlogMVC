
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace BlogMVC.Repositories.Images
{
    public class CloudnaryImageRepositorio : IImageRepositorio
    {
        private readonly IConfiguration _configuracao;
        private readonly Account _account;

        public CloudnaryImageRepositorio(IConfiguration configuracao)
        {
            _configuracao = configuracao;
            _account = new Account(
                configuracao.GetSection("Cloudinary")["CloudName"],
                configuracao.GetSection("Cloudinary")["ApiKey"],
                configuracao.GetSection("Cloudinary")["ApiSecret"]);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.Name
            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
