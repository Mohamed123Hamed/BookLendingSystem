using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ResultBehaviour<string>>
    {
        private readonly IBookService _bookService;

        public AddBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ResultBehaviour<string>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Domain.Entites.Book
            {
                Title = request.Title,
                Author = request.Author,
                IsAvailable = true
            };
            await _bookService.AddBookAsync(book);
            return ResultBehaviour<string>.Success("Book added successfully.");
        }

    }
}
