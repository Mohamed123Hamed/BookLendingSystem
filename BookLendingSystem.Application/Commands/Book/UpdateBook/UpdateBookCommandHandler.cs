using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ResultBehaviour<string>>
    {
        private readonly IBookService _bookService;

        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ResultBehaviour<string>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.Id);
            if (book == null)
                return ResultBehaviour<string>.Failure("Book not found.");

            book.Title = request.Title;
            book.Author = request.Author;

            await _bookService.UpdateBookAsync(book);
            return ResultBehaviour<string>.Success("Book updated successfully.");
        }
    }
}
