namespace LibraryWebApp.Application.DTOs.AuthDTOs
{
    public class LoginResponseViewModel
    {
        public bool IsLoggedIn { get; set; } = false;
        public string AccessToken { get; set; }
        public string RefreshToken { get; internal set; }
    }
}
