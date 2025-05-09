using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.UpdateBook
{
    public class UpdateBookCommand : IRequest<ResultBehaviour<string>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}
