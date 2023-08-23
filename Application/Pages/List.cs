using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
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
            private readonly IUserAccessor _userAccessor;

            public Handler(IPageRepository pageRepository, IUserAccessor userAccessor)
            {
                _pageRepository = pageRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<PageList<Page>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _pageRepository.IfUserHasAccessToThePages(request.UnitId, _userAccessor.GetUserId()))
                {
                    return Result<PageList<Page>>.Failure("You have no access to this data");
                }

                int count = await _pageRepository.GetOwnedCountAsync(request.UnitId);

                var pages = await _pageRepository.GetAllFilteredAsync(request.UnitId, request.RequestDto);

                var pagesPaged = new PageList<Page>(pages, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Page>>.Success(pagesPaged);
            }
        }
    }
}
