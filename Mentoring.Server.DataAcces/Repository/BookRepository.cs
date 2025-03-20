using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;
using Mentoring.Server.DataAcces.Context;
using Microsoft.EntityFrameworkCore;


namespace Mentoring.Server.DataAcces.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;
        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task<Book> GetBooksByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateBookAsync(int id, Book updateBook)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBook == null)
            {
                return null; 
            }


            existingBook.Title = updateBook.Title;
            existingBook.Description = updateBook.Description;
            existingBook.Author = updateBook.Author;

            await _context.SaveChangesAsync();
            return existingBook;
        }
        public async Task<int> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return 0;
            }

            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
            

        }
    }
}
