using BookLendingSystem.Domain.Identity;

namespace BookLendingSystem.Domain.Entites
{
    public class BookBorrowing
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Book? Book { get; set; }
        public void ReturnBook()
        {
            IsReturned = true;
            Book.IsAvailable = true;
        }

        public bool IsOverdue()
        {
            return !IsReturned && DateTime.Now > DueDate;
        }
    }


    public class BorrowingSetting
    { 
        public int BorrowDurationInDays { get; set; }
    }
     
}
 