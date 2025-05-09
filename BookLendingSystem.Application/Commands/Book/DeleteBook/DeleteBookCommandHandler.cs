using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ResultBehaviour<string>>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ResultBehaviour<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.Id);
            if (book == null)
                return ResultBehaviour<string>.Failure("Book not found.");

            await _bookService.DeleteBookAsync(book);
            return ResultBehaviour<string>.Success("Book deleted successfully.");
        }
    }
}
