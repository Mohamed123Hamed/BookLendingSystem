using BookLendingSystem.Application.DTOs;
using MediatR;

namespace BookLendingSystem.Application.Queries.GetUsers
{
    public class GetAllBooksQuery :IRequest<IReadOnlyList<BookDto>>
    {
    }

}
