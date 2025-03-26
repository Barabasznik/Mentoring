using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;
using Mentoring.Application.Result;

namespace Mentoring.Application.Queries
{
    public class GetBookByIdQuery : IRequest<OperationResult<Book>>
    {
        public int Id { get; set; }
    }
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBooksByIdAsync(request.Id);
            if (book == null)
            {
                return OperationResult<Book>.Failure("Book not found");
            }
            return OperationResult<Book>.Success(book);
        }
    }

}
