using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApp.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IMemoryCache _memoryCache;
        public ImageService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
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
        public async Task<string> GetImageAsync(string imageBook, string _imagePath)
        {
            var imageKey = Path.GetFileName(imageBook);
            var image = GetImage(imageKey);

            if (image == null)
            {
                var imagePath = Path.Combine(_imagePath, imageKey);
                if (File.Exists(imagePath))
                {
                    image = await File.ReadAllBytesAsync(imagePath);
                    SetImage(imageKey, image, TimeSpan.FromMinutes(10));
                }
            }

            return imageKey;
        }
        public byte[] GetImage(string key)
        {
            _memoryCache.TryGetValue(key, out byte[] image);
            return image;
        }

        public void SetImage(string key, byte[] image, TimeSpan cacheDuration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(cacheDuration);

            _memoryCache.Set(key, image, cacheEntryOptions);
        }
    }
}
