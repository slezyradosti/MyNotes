using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Details
    {
        public class Query : IRequest<Result<Page>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Page>>
        {
            private readonly IPageRepository _pageRepository;

            public Handler(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<Result<Page>> Handle(Query request, CancellationToken cancellationToken)
            {
                var page = await _pageRepository.GetOneWithItsNotesAsync(request.Id);

                return Result<Page>.Success(page);
            }
        }
    }
}
