using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;
using Mentoring.Application.Result;

namespace Mentoring.Application.Commands
{
    public class UpdateBookCommand : IRequest<OperationResult<Book>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;
        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.GetBooksByIdAsync(request.Id);
            if (existingBook == null)
            {
                return OperationResult<Book>.Failure("Book not found");
            }
            existingBook.Title = request.Title;
            existingBook.Author = request.Author;
            existingBook.Description = request.Description;

            var updatedBook = await _bookRepository.UpdateBookAsync(request.Id, existingBook);
            return OperationResult<Book>.Success(updatedBook);
        }
    }

}
