using BookLendingSystem.Domain.Identity;

namespace BookLendingSystem.Application.Services.AuthService
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);

    }
}
