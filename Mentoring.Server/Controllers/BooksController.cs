using MediatR;
using Mentoring.Application.Commands;
using Mentoring.Application.Queries;
using Mentoring.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace Mentoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
    


        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            var result = await _mediator.Send(new GetBooksQuery());
            return result;
        }

        [HttpGet(("Id"))]
        public async Task<ActionResult<Book>> GetById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound(new { message = "Książka nie została znaleziona" });
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] PostBookCommand newBook)
        {
            var book = await _mediator.Send(newBook);

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] UpdateBookCommand updatedBook)
        {
            var result = await _mediator.Send(updatedBook);
            if (result == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteBookCommand { Id = id };
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound(new { message = "Książka o podanym ID nie została znaleziona." });
            }


        }
    }

}
