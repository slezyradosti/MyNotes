using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class List
    {
        public class Query : IRequest<Result<PageList<Notebook>>> 
        { 
            public RequestDto RequestDto { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PageList<Notebook>>>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INotebookRepository notebookRepository, IUserAccessor userAccessor)
            {
                _notebookRepository = notebookRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<PageList<Notebook>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                int count = await _notebookRepository.GetOwnedCountAsync(userId);
                
                var notebooks = await _notebookRepository.GetUsersAllFilteredAsync(userId,
                    request.RequestDto);

                var notebooksPaged = new PageList<Notebook>(notebooks, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Notebook>>.Success(notebooksPaged);
            }
        }
    }
}
