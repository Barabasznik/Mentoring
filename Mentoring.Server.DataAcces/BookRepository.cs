using Mentoring.Server.DataAcces.Context;
using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;


namespace Mentoring.Server.DataAcces
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;
        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }
        private readonly List<Book> _books = new()
        {
            new() { Title = "wahtever", Description = "BLABLA BLA BLA BLA", Id = 1},
            new() { Title = "wahtever2", Description = "BLABLA BLA BLA BLA222", Id = 2}
        };
        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBooksById(int id)
        {
            return _context.Books.FirstOrDefault(book => book.Id == id);
        }

        public Book AddBook(Book book) 
        {
            _context.Books.Add(book);
                _context.SaveChanges();
            return book;
        }
        
    }
}
