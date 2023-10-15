using Application.Core;
using Application.DTOs;
using Domain.Repositories.Repos;
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
            private readonly INotebookRepository _notebookRepository;

            public Handler(IUnitRepository unitRepository, IUserAccessor userAccessor,
                INotebookRepository notebookRepository)
            {
                _unitRepository = unitRepository;
                _userAccessor = userAccessor;
                _notebookRepository = notebookRepository;
            }
            public async Task<Result<PageList<Unit>>> Handle(Query request, CancellationToken cancellationToken)
            {
                

                //if (count > 0)
                //{
                //    if (!await _unitRepository.IfUserHasAccessToTheUnits(request.NotebookId, _userAccessor.GetUserId()))
                //    {
                //        return Result<PageList<Unit>>.Failure("You have no access to this data");
                //    }
                //}
                //else
                {
                    if (!await _notebookRepository.IfUserHasAccessToTheNotebook(request.NotebookId, _userAccessor.GetUserId()))
                    {
                        return Result<PageList<Unit>>.Failure("You have no access to this data");
                    }
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
