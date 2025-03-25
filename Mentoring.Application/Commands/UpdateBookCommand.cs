using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;

namespace Mentoring.Application.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;
        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _bookRepository.GetBooksByIdAsync(request.Id);
            if (existingBook == null) { return null; }
            existingBook.Title = request.Title;
            existingBook.Author = request.Author;
            existingBook.Description = request.Description;

            return await _bookRepository.UpdateBookAsync(request.Id, existingBook);
        }
    }

}
