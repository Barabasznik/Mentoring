using Mentoring.Server.DataAcces.Models;

namespace Mentoring.Server.DataAcces.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
    }
    internal class BookRepository : IBookRepository
    {
        public List<Book> GetBooks()       //do osobnego pliku
        {
            return new List<Book>() { new Book() { Title = "wahtever"  }   };
            
        }

    }
}
