using Microsoft.AspNetCore.Http;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> CreateImageAsync(IFormFile titleImage, string path);
        Task<string> GetImageAsync(string imageBook, string _imagePath);
    }
}