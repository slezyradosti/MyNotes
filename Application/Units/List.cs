using Application.Core;
using Application.DTOs;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class List
    {
        public record Query(Guid NotebookId, RequestDto RequestDto) : IRequest<Result<PageList<Unit>>>;

        public class Handler : IRequestHandler<Query, Result<PageList<Unit>>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitRepository unitRepository, IUserAccessor userAccessor)
            {
                _unitRepository = unitRepository;
                _userAccessor = userAccessor;
            }
            public async Task<Result<PageList<Unit>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _unitRepository.IfUserHasAccessToTheUnits(request.NotebookId, _userAccessor.GetUserId()))
                {
                    return Result<PageList<Unit>>.Failure("You have no access to this data");
                }

                int count = await _unitRepository.GetOwnedCountAsync(request.NotebookId);

                var units = await _unitRepository.GetAllFilteredAsync(request.NotebookId, request.RequestDto);

                var unitsPaged = new PageList<Unit>(units, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Unit>>.Success(unitsPaged);
            }
        }
    }
}
