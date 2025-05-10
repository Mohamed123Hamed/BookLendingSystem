using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Book.AddBook
{
    public class AddBookCommand : IRequest<ResultBehaviour<string>>
    {
    public string Title { get; set; }
    public string Author { get; set; }
        public int Quantity { get; set; }

    }
}
