using LibraryWebApp.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IIssueBookUseCase
    {
        Task<IActionResult> ExecuteAsync(IssueBookViewModel viewModel);
    }
}