using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;

namespace Mentoring.Application.Commands
{
   public class PostBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
    public class PostBookCommandHandler : IRequestHandler<PostBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;
        public PostBookCommandHandler(IBookRepository repository)
        {
            _bookRepository = repository;
        }
        public async Task<Book> Handle(PostBookCommand post, CancellationToken cancellationToken)
        {
            var book = new Book()
            {
                Title = post.Title,
                Description = post.Description,
                Author = post.Author
            };
            var createBook = await _bookRepository.AddBookAsync(book);
            return createBook;
        }
    }

}
