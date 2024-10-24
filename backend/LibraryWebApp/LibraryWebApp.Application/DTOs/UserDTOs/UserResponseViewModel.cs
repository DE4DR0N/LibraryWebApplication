using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.DTOs.UserDTOs
{
    public record UserResponseViewModel(Guid Id, string UserName, string PasswordHash, string Role, string? RefreshToken,
                                        DateTime RefreshTokenExpiry, ICollection<BookResponseViewModel>? Books);
}
