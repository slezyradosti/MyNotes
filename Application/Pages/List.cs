using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class List
    {
        public class Query : IRequest<Result<List<Page>>>
        {
            public Guid UnitId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Page>>>
        {
            private readonly IPageRepository _pageRepository;

            public Handler(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<Result<List<Page>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pages = await _pageRepository.GetAllFromOneUnitAsync(request.UnitId);

                return Result<List<Page>>.Success(pages);
            }
        }
    }
}
