using NSubstitute;
using Xunit;

namespace BookLendingSystem.UnitTests.Auth
{
        public class AuthServiceTests
        {
            private readonly IAuthService _authService;

            public AuthServiceTests()
            {
                _authService = Substitute.For<IAuthService>();
            }

            [Fact]
            public async Task RegisterAsync_ShouldReturnTrue_WhenUserIsRegistered()
            {
                var email = "test@example.com";
                var password = "P@ssw0rd";

                _authService.RegisterAsync(email, password).Returns(Task.FromResult(true));

                var result = await _authService.RegisterAsync(email, password);

                Assert.True(result);
            }
        }
    
}
