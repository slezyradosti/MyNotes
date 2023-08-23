using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Delete
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPageRepository _pageRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(IPageRepository pageRepository, IUserAccessor userAccessor)
            {
                _pageRepository = pageRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var page = await _pageRepository.GetOneAsync(request.Id);

                if (page == null) return null;

                if (!await _pageRepository.IfUserHasAccessToThePage(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var result = await _pageRepository.RemoveAsync(page) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete page");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
