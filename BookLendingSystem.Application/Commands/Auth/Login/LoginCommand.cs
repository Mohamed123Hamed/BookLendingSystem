using BookLendingSystem.Application.Common;
using MediatR;

namespace BookLendingSystem.Application.Commands.Auth.Login
{
    public class LoginCommand : IRequest<ResultBehaviour<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    public class GetUserToken
    {
        public bool IsAuthenticated { get; set; }
        public bool IsConfirmEmail { get; set; }
        public string Token { get; set; }
        public string? Message { get; set; }
        public DateTime? ExpiresOn { get; set; }

    }
}
