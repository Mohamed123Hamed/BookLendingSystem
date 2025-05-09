using BookLendingSystem.Application.Common;
using BookLendingSystem.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLendingSystem.Application.Commands.Auth.ChangeAdminRole;
public class ToggleAdminRoleCommandHandler : IRequestHandler<ToggleAdminRoleCommand, ResultBehaviour<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ToggleAdminRoleCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResultBehaviour<string>> Handle(ToggleAdminRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return ResultBehaviour<string>.Failure("User not found.");
        }

        user.IsAdmin = !user.IsAdmin;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return ResultBehaviour<string>.Failure("Failed to update user");
        }

        var status = user.IsAdmin ? "Admin" : "Member";
        return ResultBehaviour<string>.Success($"User status successfully updated to '{status}'.");
    }

}

