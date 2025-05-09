using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.DeleteBook
{
    public class DeleteBookCommand : IRequest<ResultBehaviour<string>>
    {
        public int Id { get; set; }
    }
}
