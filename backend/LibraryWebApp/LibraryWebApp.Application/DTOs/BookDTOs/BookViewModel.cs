﻿using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public long ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public Guid AuthorId { get; set; }
        public AuthorViewModel Author { get; set; }
        public Guid? UserId { get; set; }
        public DateOnly? BorrowDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}