using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;

namespace Mentoring.Application.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }
    }
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookByIdQueryHandler(IBookRepository repository)
        {
            _bookRepository = repository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBooksByIdAsync(request.Id);
        }
    }

}
