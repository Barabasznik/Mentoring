using MediatR;
using Mentoring.Application.Commands;
using Mentoring.Application.Queries;
using Mentoring.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Mentoring.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize()]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
    


        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var result = await _mediator.Send(new GetBooksQuery());
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpGet(("Id"))]
        public async Task<ActionResult<Book>> GetById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery { Id = id });
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] PostBookCommand newBook)
        {
            var book = await _mediator.Send(newBook);

            if (book.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = book.Value.Id }, book.Value);
            }
            return BadRequest(book.Error);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] UpdateBookCommand updatedBook)
        {
            var result = await _mediator.Send(updatedBook);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var command = await _mediator.Send(new DeleteBookCommand{ Id = id });
                if (command.IsSuccess)
                {
                    return Ok();
                }
                return NotFound(new { message = command.Error });

        }
    }

}
