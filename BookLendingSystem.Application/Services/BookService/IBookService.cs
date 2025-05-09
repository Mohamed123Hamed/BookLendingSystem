using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Domain.Entites;

namespace BookLendingSystem.Application.Services.BookService
{
    public interface IBookService
    {
        Task<IReadOnlyList<BookDto>> GetAllBooksAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<Book?> GetBorrowedBookByUserIdAsync(string userId);
        Task<List<Book>> GetOverdueBooksAsync();
        Task<List<BookBorrowing>> GetOverdueBorrowingsAsync();
        Task BorrowBookAsync(int bookId, string userId);
    }
}
