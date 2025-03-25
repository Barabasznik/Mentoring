using MediatR;
using Mentoring.Application.Interfaces;

namespace Mentoring.Application.Commands
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteBookAsync(request.Id);
            if (result == 0)
            {
                throw new Exception("Nie znaleziono książki do usunięcia");
            }

        }
    }
}
