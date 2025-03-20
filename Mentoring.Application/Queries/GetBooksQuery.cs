using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mentoring.Application.Interfaces;
using Mentoring.Domain.Models;

namespace Mentoring.Application.Queries
{
   public class GetBooksQuery : IRequest<List<Book>>
    {

    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _bookRepository = repository;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBooksAsync();
        }
    }

}
