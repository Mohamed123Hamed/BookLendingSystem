namespace BookLendingSystem.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }

        public string? BorrowedByUserId { get; set; }

    }
}
