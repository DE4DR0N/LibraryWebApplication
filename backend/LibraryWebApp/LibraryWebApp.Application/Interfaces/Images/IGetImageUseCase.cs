namespace LibraryWebApp.Application.Interfaces.Images
{
    public interface IGetImageUseCase
    {
        Task<string> ExecuteAsync(string imageBook, string imagePath);
    }
}