using System.Security.Cryptography;
using LibraryWebApp.Application.Interfaces.Tokens;

namespace LibraryWebApp.Application.UseCases.Tokens
{
    public class GenerateRefreshTokenUseCase : IGenerateRefreshTokenUseCase
    {
        public string Execute()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
