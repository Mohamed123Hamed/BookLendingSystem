using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Queries.GetBookByIdQuery
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly IBookService _bookService;

        public GetBookByIdQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.Id);
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsAvailable = book.IsAvailable
            };
        }
    }
}