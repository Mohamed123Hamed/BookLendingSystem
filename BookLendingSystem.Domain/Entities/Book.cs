using BookLendingSystem.Domain.Identity;

namespace BookLendingSystem.Domain.Entites
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;

        public bool IsAvailable { get; set; } = true;
        public ApplicationUser? BorrowedByUser { get; set; }
        public string? BorrowedByUserId { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
