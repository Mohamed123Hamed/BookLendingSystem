using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLendingSystem.Application.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResultBehaviour<List<UserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResultBehaviour<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();

            if (users == null || !users.Any())
            {
                return ResultBehaviour<List<UserDto>>.Failure("No users found.");
            }

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                PhoneNumber = user.PhoneNumber
            }).ToList();

            return ResultBehaviour<List<UserDto>>.Success("Users retrieved successfully.", userDtos);
        }
    }
}