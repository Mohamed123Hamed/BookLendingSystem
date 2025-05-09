using BookLendingSystem.Application.Jobs;
using BookLendingSystem.Application.Services.BookService;
using BookLendingSystem.Application.Services.EmailService;
using BookLendingSystem.Domain.Entites;
using NSubstitute;
using Xunit;
namespace BookLendingSystem.UnitTests.Jobs
{
    public class BooksJobTests
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;
        private readonly BooksJob _job;

        public BooksJobTests()
        {
            _bookService = Substitute.For<IBookService>();
            _emailService = Substitute.For<IEmailService>();
            _job = new BooksJob(_bookService, _emailService);
        }

        [Fact]
        public async Task CheckOverdueBooks_Should_Send_Email_If_Books_Overdue()
        {
            var books = new List<Book>
        {
            new Book { Title = "Late Book", BorrowedByUser = new Domain.Identity.ApplicationUser { Email = "user@mail.com", UserName = "user" }, BorrowedDate = DateTime.UtcNow.AddMinutes(-10), DueDate = DateTime.UtcNow.AddMinutes(-5) }
        };

            _bookService.GetOverdueBooksAsync().Returns(books);

            await _job.CheckOverdueBooks();

            await _emailService.Received(1).SendEmailAsync(
                "user@mail.com",
                Arg.Any<string>(),
                Arg.Is<string>(body => body.Contains("Late Book"))
            );
        }
    }
}
