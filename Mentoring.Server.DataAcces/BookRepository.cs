using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;


namespace Mentoring.Server.DataAcces
{
    internal class BookRepository : IBookRepository
    {

        public List<Book> GetBooks()
        {
            return new List<Book> { new() { Title = "wahtever", Description = "BLABLA BLA BLA BLA", Id = 1} };

        }

        public List<Book> GetBooksByID()
        {
            return new List<Book> { new() { Id = 2 } };
        }
        
    }
}
