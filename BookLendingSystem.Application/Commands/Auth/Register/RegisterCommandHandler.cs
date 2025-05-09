using BookLendingSystem.Application.Common;
using BookLendingSystem.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLendingSystem.Application.Commands.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultBehaviour<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public RegisterCommandHandler(UserManager<ApplicationUser> userManager
)
        {
            _userManager = userManager;
        }
        public async Task<ResultBehaviour<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var validator = new RegisterCommandValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ResultBehaviour<string>(false, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return ResultBehaviour<string>.Failure("This email is already registered.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return ResultBehaviour<string>.Failure("Password and Confirm Password do not match.");
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                IsAdmin = request.IsAdmin
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return ResultBehaviour<string>.Failure(errors);
            }

            return ResultBehaviour<string>.Success("User registered successfully", user.Id);
        }
    }
}
