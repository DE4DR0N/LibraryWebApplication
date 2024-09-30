namespace LibraryWebApp.Application.DTOs
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<BookViewModel>? Books { get; set; }
    }
}
