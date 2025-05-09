using BookLendingSystem.Application.Commands.Auth.ChangeAdminRole;
using BookLendingSystem.Application.Commands.Auth.Login;
using BookLendingSystem.Application.Commands.Auth.Register;
using BookLendingSystem.Application.DTOs;
using BookLendingSystem.Application.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingSystem.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers() 
             => Ok(await _mediator.Send(new GetUsersQuery()));

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
         => Ok(await _mediator.Send(command));


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
             => Ok(await _mediator.Send(command));

        [HttpPost("ToggleAdminRole")]
        public async Task<IActionResult> ToggleAdminRole([FromBody] ToggleAdminRoleCommand command)
              => Ok(await _mediator.Send(command));


    }
}
