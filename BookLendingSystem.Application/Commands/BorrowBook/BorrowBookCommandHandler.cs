using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.BookService;
using MediatR;

namespace BookLendingSystem.Application.Commands.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, ResultBehaviour<string>>
    {
        private readonly IBookService _bookService;

        public BorrowBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ResultBehaviour<string>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(request.BookId);
            if (book is null)
                return ResultBehaviour<string>.Failure("Book not found.");

            if (!book.IsAvailable)
                return ResultBehaviour<string>.Failure("Book is already borrowed.");

            var existingBorrowedBook = await _bookService.GetBorrowedBookByUserIdAsync(request.UserId);
            if (existingBorrowedBook != null)
                return ResultBehaviour<string>.Failure("You can only borrow one book at a time.");


            book.IsAvailable = false;
            book.BorrowedByUserId = request.UserId;
            book.BorrowedDate = DateTime.UtcNow;
            book.DueDate = DateTime.UtcNow.AddDays(7);

            await _bookService.UpdateBookAsync(book);

            return ResultBehaviour<string>.Success("Book borrowed successfully.");
        }
    }
}
