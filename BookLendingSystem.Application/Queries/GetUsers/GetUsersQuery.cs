using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.DTOs;
using MediatR;

namespace BookLendingSystem.Application.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<ResultBehaviour<List<UserDto>>>
    {

    }

}
