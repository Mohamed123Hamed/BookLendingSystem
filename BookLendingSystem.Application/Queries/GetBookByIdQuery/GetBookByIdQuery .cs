using BookLendingSystem.Application.DTOs;
using MediatR;

namespace BookLendingSystem.Application.Queries.GetBookByIdQuery
{
    public class GetBookByIdQuery : IRequest<BookDto?>
    {
        public int Id { get; set; }

    }

}
