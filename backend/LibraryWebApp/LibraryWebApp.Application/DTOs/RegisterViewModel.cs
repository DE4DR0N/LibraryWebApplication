﻿namespace LibraryWebApp.Application.DTOs
{
    public class RegisterViewModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}