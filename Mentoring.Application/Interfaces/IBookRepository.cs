using Mentoring.Domain.Models;
namespace Mentoring.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync();  
        Task<Book> GetBooksByIdAsync(int id);  
        Task<Book> AddBookAsync(Book book);  
        Task<Book> UpdateBookAsync(int id, Book updateBook);  
        Task<int> DeleteBookAsync(int id);  
    }


}
