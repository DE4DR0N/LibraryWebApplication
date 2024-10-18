﻿using LibraryWebApp.Application.Interfaces.Images;
using Microsoft.AspNetCore.Http;

namespace LibraryWebApp.Application.UseCases.Images
{
    public class CreateImageUseCase : ICreateImageUseCase
    {
        public async Task<string> ExecuteAsync(IFormFile titleImage, string path)
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
