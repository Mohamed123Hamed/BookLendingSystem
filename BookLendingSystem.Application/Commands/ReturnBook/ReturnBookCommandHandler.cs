using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, ResultBehaviour<string>>
    {
        private readonly IBookService _bookService;

        public ReturnBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ResultBehaviour<string>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.BookId);
            if (book is null)
                return ResultBehaviour<string>.Failure("Book not found.");

            if (book.IsAvailable)
                return ResultBehaviour<string>.Failure("This book is already available.");

            if (book.BorrowedByUserId != request.UserId)
                return ResultBehaviour<string>.Failure("You are not the borrower of this book.");

            book.IsAvailable = true;
            book.BorrowedByUserId = null;
            book.BorrowedDate = null;
            book.DueDate = null;

            await _bookService.UpdateBookAsync(book);

            return ResultBehaviour<string>.Success("Book returned successfully.");
        }
    }
}
