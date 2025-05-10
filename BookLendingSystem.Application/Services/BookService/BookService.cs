using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Domain.Entites;
using BookLendingSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace BookLendingSystem.Application.Services.BookService
{
    public class BookService : IBookService
{
    private readonly AppDbContext _context;
        private readonly BorrowingSetting _borrowingSetting;

        public BookService(AppDbContext context, IOptions<BorrowingSetting> borrowingSetting)
        {
            _context = context;
            _borrowingSetting = borrowingSetting.Value;
        }

        public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
        {
            return await _context.Books.AsNoTracking()
                .Select(x => new BookDto {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Quantity = x.Quantity,
                    IsAvailable = x.IsAvailable
                }).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.AsNoTracking()
                                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetBorrowedBookByUserIdAsync(string userId)
        {
            return await _context.Books
                                 .FirstOrDefaultAsync(b => b.BorrowedByUserId == userId && !b.IsAvailable);
        }

        public async Task<List<Book>> GetOverdueBooksAsync()
        {
            return await _context.Books
                .Include(b => b.BorrowedByUser)
                .Where(b => b.BorrowedDate.HasValue && b.DueDate.HasValue && b.DueDate.Value < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task BorrowBookAsync(int bookId, string userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                throw new Exception("Book not found");

            if (book.IsAvailable == false)
                throw new Exception("This book is already borrowed.");

            var borrowing = new BookBorrowing
            {
                BookId = bookId,
                UserId = userId,
                BorrowedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(_borrowingSetting.BorrowDurationInDays),
                IsReturned = false
            };

            _context.BookBorrowings.Add(borrowing);
            book.IsAvailable = false;
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookBorrowing>> GetOverdueBorrowingsAsync()
        {
            return await _context.BookBorrowings
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => !b.IsReturned && b.DueDate < DateTime.UtcNow)
                .ToListAsync();
        }

    }
}
