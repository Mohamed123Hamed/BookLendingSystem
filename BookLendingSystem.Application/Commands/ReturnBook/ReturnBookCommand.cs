using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest<ResultBehaviour<string>>
    {
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
