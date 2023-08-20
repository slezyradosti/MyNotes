using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class List
    {
        public class Query : IRequest<Result<PageList<Page>>>
        {
            public Guid UnitId { get; set; }
            public RequestDto RequestDto { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PageList<Page>>>
        {
            private readonly IPageRepository _pageRepository;

            public Handler(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<Result<PageList<Page>>> Handle(Query request, CancellationToken cancellationToken)
            {
                int count = await _pageRepository.GetCountAsync();

                var pages = await _pageRepository.GetAllFilteredAsync(request.UnitId, request.RequestDto);

                var pagesPaged = new PageList<Page>(pages, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Page>>.Success(pagesPaged);
            }
        }
    }
}
