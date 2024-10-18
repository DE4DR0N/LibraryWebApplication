using Microsoft.AspNetCore.Http;

namespace LibraryWebApp.Application.Interfaces.Images
{
    public interface ICreateImageUseCase
    {
        Task<string> ExecuteAsync(IFormFile titleImage, string path);
    }
}