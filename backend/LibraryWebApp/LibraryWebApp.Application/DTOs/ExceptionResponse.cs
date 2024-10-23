using System.Net;

namespace LibraryWebApp.Application.DTOs
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
