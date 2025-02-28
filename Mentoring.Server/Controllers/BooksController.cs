using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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



        //GET: api/<BooksController>


        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet("Id")]
        public ActionResult<Book> GetById([FromQuery]int id)
        {
            var book = _bookRepository.GetBooksById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }


        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var newbook = _bookRepository.AddBook(book);
            return Ok(newbook);
        }

        //// PUT api/<BooksController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BooksController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
