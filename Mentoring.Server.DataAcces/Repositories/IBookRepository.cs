using Mentoring.Server.DataAcces.Models;

namespace Mentoring.Server.DataAcces.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        public List<Book> GetBooksById();

    }


}
