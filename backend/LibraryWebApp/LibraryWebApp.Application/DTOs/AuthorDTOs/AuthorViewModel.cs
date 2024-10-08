﻿namespace LibraryWebApp.Application.DTOs.AuthorDTOs
{
    public class AuthorViewModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Country { get; set; }
    }
}
