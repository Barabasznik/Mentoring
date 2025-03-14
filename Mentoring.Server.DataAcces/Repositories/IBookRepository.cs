﻿using Mentoring.Server.DataAcces.Models;

namespace Mentoring.Server.DataAcces.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        Book GetBooksById(int id);
        Book AddBook(Book book);
        Book UpdateBook(int id, Book updateBook);
        int DeleteBook(int id);

    }


}
