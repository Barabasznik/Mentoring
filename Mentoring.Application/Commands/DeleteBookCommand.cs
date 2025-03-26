using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Application.Result;

namespace Mentoring.Application.Commands
{
    public class DeleteBookCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.DeleteBookAsync(request.Id);
            if (result == 0)
            {
                return OperationResult.Failure("Nie znaleziono książki do usunięcia");
            }

            return OperationResult.Success();
        }
    }
}