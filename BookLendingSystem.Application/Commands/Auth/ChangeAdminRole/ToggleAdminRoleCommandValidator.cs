using BookLendingSystem.Application.Common;
using FluentValidation;

namespace BookLendingSystem.Application.Commands.Auth.ChangeAdminRole;
public class ToggleAdminRoleCommandValidator : AbstractValidator<ResultBehaviour<string>>
{

    public ToggleAdminRoleCommandValidator()
    {
  
    }
}
