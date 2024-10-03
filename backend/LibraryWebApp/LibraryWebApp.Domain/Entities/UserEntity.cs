namespace LibraryWebApp.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public required string Role {  get; set; }
        public ICollection<BookEntity>? Books { get; set; }
    }
}
