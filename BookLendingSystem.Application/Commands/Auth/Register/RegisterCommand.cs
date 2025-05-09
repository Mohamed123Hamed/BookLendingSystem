using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Auth.Register
{
    public class RegisterCommand : IRequest<ResultBehaviour<string>>
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
