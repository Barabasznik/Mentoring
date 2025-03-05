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


        public Book UpdateBook(int id, Book updatedBook)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = updatedBook.Title;
            existingBook.Description = updatedBook.Description;
            existingBook.Author = updatedBook.Author;

            _context.SaveChanges();
            return existingBook;
        }


    }
}
