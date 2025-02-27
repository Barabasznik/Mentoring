using Mentoring.Server.DataAcces.Models;

namespace Mentoring.Server.DataAcces.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        Book GetBooksById(int id);

    }


}
