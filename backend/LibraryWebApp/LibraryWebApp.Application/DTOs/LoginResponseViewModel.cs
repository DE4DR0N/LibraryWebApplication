namespace LibraryWebApp.Application.DTOs
{
    public class LoginResponseViewModel
    {
        public bool IsLoggedIn { get; set; } = false;
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
    }

}
