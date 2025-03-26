using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Application.Result;
using Mentoring.Domain.Models;

namespace Mentoring.Application.Commands
{
   public class PostBookCommand : IRequest<OperationResult<Book>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
    public class PostBookCommandHandler : IRequestHandler<PostBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        public PostBookCommandHandler(IBookRepository repository)
        {
            _bookRepository = repository;
        }
        public async Task<OperationResult<Book>> Handle(PostBookCommand post, CancellationToken cancellationToken)
        {

            var book = new Book()
            {
                Title = post.Title,
                Description = post.Description,
                Author = post.Author
            };
            var createBook = await _bookRepository.AddBookAsync(book);
            return OperationResult<Book>.Success(createBook);
        }
    }

}
