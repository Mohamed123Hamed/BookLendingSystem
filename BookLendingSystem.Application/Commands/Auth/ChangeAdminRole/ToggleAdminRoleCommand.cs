using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Auth.ChangeAdminRole;
public record ToggleAdminRoleCommand : IRequest<ResultBehaviour<string>>
{
    public string? Email { get; set; }
}


