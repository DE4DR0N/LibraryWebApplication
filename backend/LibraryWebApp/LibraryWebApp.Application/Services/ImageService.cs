using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraryWebApp.Application.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> CreateImageAsync(IFormFile titleImage, string path)
        {
            var fileName = Path.GetFileName(titleImage.FileName);
            var filePath = Path.Combine(path, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await titleImage.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
