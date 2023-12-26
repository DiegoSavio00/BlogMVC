namespace BlogMVC.Repositories.Images
{
    public interface IImageRepositorio
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
