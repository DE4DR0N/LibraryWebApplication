using System.Security.Claims;

namespace LibraryWebApp.Application.Interfaces.Tokens
{
    public interface IGenerateAccessTokenUseCase
    {
        string Execute(IEnumerable<Claim> claims);
    }
}