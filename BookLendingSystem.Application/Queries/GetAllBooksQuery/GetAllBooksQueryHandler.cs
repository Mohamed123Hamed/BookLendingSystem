using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Queries.GetUsers
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IReadOnlyList<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetAllBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IReadOnlyList<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookService.GetAllBooksAsync();
        }
    }
}