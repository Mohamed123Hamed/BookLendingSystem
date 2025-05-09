using BookLendingSystem.Application.Common;
using BookLendingSystem.Application.Services.AuthService;
using BookLendingSystem.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLendingSystem.Application.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultBehaviour<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ITokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
  
        public async Task<ResultBehaviour<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var validator = new LoginCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new ResultBehaviour<string>(false, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }


            if (string.IsNullOrWhiteSpace(request.Email))
                return ResultBehaviour<string>.Failure("Email is required.");

            if (string.IsNullOrWhiteSpace(request.Password))
                return ResultBehaviour<string>.Failure("Password is required.");
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return ResultBehaviour<string>.Failure("Invalid Email or Password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                return ResultBehaviour<string>.Failure("Invalid Email or Password");

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            return ResultBehaviour<string>.Success("Login Successful", token);
        }
    }
}