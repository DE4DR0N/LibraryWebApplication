﻿using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> ExecuteAsync(Guid id, BookViewModel bookDto)
        {
            var entBook = _unitOfWork.Books.GetByIdAsync(id);
            if (entBook == null) return new NotFoundResult();
            var book = _mapper.Map<BookEntity>(bookDto);
            book.Id = id;
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
            return new NoContentResult();
        }
    }
}
