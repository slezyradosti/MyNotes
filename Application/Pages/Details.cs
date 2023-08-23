using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
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
            private readonly IUserAccessor _userAccessor;

            public Handler(IPageRepository pageRepository, IUserAccessor userAccessor)
            {
                _pageRepository = pageRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Page>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _pageRepository.IfUserHasAccessToThePage(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<Page>.Failure("You have no access to this data");
                }

                var page = await _pageRepository.GetOneWithItsNotesAsync(request.Id);

                return Result<Page>.Success(page);
            }
        }
    }
}
