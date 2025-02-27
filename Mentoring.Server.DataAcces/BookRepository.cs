using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;


namespace Mentoring.Server.DataAcces
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new()
        {
            new() { Title = "wahtever", Description = "BLABLA BLA BLA BLA", Id = 1},
            new() { Title = "wahtever2", Description = "BLABLA BLA BLA BLA222", Id = 2}
        };
        public List<Book> GetBooks()
        {
            return _books;
        }

        public Book GetBooksById(int id)
        {
            return _books.FirstOrDefault(book => book.Id == id);
        }

    }
}
