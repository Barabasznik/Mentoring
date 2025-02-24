using Mentoring.Server.DataAcces.Models;
using Mentoring.Server.DataAcces.Repositories;


namespace Mentoring.Server.DataAcces
{
    internal class BookRepository : IBookRepository
    {
        public List<Book> GetBooks()
        {
            return new List<Book>() { new Book() { Title = "wahtever" } };

        }
    }
}
