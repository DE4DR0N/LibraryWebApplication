using LibraryWebApp.Application.Interfaces.Images;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApp.Application.UseCases.Images
{
    public class GetImageUseCase : IGetImageUseCase
    {
        private readonly IMemoryCache _memoryCache;
        public GetImageUseCase(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<string> ExecuteAsync(string imageBook, string _imagePath)
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
        private byte[] GetImage(string key)
        {
            _memoryCache.TryGetValue(key, out byte[] image);
            return image;
        }

        private void SetImage(string key, byte[] image, TimeSpan cacheDuration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(cacheDuration);

            _memoryCache.Set(key, image, cacheEntryOptions);
        }
    }
}
