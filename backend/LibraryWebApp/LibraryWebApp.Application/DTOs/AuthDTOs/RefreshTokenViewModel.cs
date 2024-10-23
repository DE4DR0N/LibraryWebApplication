namespace LibraryWebApp.Application.DTOs.AuthDTOs
{
    public record RefreshTokenViewModel(
        string JwtToken,
        string RefreshToken);
}
