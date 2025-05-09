using BookLendingSystem.Application.Services.BookService;
using BookLendingSystem.Domain.Entites;
using BookLendingSystem.Infrastructure.Context;
using Microsoft.Extensions.Options;
using Xunit;

namespace BookLendingSystem.UnitTests.Services
{
    public class BookServiceTests
    {
        private readonly AppDbContext _context;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _context = TestDbContextFactory.Create();
            var options = Options.Create(new BorrowingSetting { BorrowDurationInDays = 5 });
            _bookService = new BookService(_context, options);
        }

        [Fact]
        public async Task AddBookAsync_Should_Add_Book_To_Database()
        {
            var book = new Book { Title = "Test Book", Author = "Tester", IsAvailable = true };

            await _bookService.AddBookAsync(book);

            var addedBook = await _context.Books.FindAsync(book.Id);
            Assert.NotNull(addedBook);
            Assert.Equal("Test Book", addedBook.Title);
        }
    }

}
