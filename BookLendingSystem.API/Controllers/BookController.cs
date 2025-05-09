using BookLendingSystem.Application.Commands.Book.AddBook;
using BookLendingSystem.Application.Commands.Book.DeleteBook;
using BookLendingSystem.Application.Commands.Book.UpdateBook;
using BookLendingSystem.Application.Commands.BorrowBook;
using BookLendingSystem.Application.Commands.ReturnBook;
using BookLendingSystem.Application.Queries.GetBookByIdQuery;
using BookLendingSystem.Application.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingSystem.API.Controllers
{
    public class BookController : BaseController
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
            => Ok(await _mediator.Send(new GetAllBooksQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
           => Ok(await _mediator.Send(new GetBookByIdQuery { Id = id }));


        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id)
                return BadRequest("Mismatched Book ID");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand { Id = id });
            return Ok(result);
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
