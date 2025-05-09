using BookLendingSystem.Application.Services.BookService;
using BookLendingSystem.Application.Services.EmailService;

namespace BookLendingSystem.Application.Jobs
{
    public class BooksJob
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;

        public BooksJob(IBookService bookService, IEmailService emailService)
        {
            _bookService = bookService;
            _emailService = emailService;
        }

        public async Task CheckOverdueBooks()
        {
            var overdueBooks = await _bookService.GetOverdueBooksAsync();

            foreach (var book in overdueBooks)
            {
                if (book.BorrowedDate.HasValue && book.DueDate.HasValue && book.DueDate.Value < DateTime.UtcNow)
                {
                    var userEmail = book.BorrowedByUser.Email;  
                    var subject = "Book Return Reminder";
                    var body = $"Dear {book.BorrowedByUser.UserName},\n\n" +
                               $"You have borrowed the book '{book.Title}' and it is now overdue. Please return it as soon as possible.\n\n" +
                               "Thank you!";

                    await _emailService.SendEmailAsync(userEmail, subject, body);
                }
            }
        }

    }
}
