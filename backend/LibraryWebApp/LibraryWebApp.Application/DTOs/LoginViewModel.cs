namespace LibraryWebApp.Application.DTOs
{
    public class LoginViewModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
