using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
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

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Result<PageList<Notebook>>> Handle(Query request, CancellationToken cancellationToken)
            {
                int count = await _notebookRepository.GetCountAsync();
                var notebooks = await _notebookRepository.GetAllFilteredAsync(request.RequestDto.PageIndex, 
                    request.RequestDto.PageSize, request.RequestDto.SortColumn, request.RequestDto.SortOrder, 
                    request.RequestDto.FilterQuery);

                var notebooksPaged = new PageList<Notebook>(notebooks, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Notebook>>.Success(notebooksPaged);
            }
        }
    }
}
