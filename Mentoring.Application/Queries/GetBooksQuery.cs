using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;
using Mentoring.Application.Result;


namespace Mentoring.Application.Queries
{
    public class GetBooksQuery : IRequest<OperationResult<IEnumerable<Book>>>
    {

    }


    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery,  OperationResult<IEnumerable<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<IEnumerable<Book>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync();
            return OperationResult<IEnumerable<Book>>.Success(books);
        }
    }

}
