using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Mentoring.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet(("Id"))]
        public ActionResult<Book> GetById([FromQuery] int id)
        {
            var book = _bookRepository.GetBooksById(id);
            if (book == null)
            {
                return NotFound(new { message = "Sorry, book not found" });
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest(new { message = "Invalid book data" });
            }

            var createdBook = _bookRepository.AddBook(newBook);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var existingBook = _bookRepository.UpdateBook(id, updatedBook);
            if (existingBook == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteBookId = _bookRepository.DeleteBook(id);

            return Ok(deleteBookId);
        }
    }

}
