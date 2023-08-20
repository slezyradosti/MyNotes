using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class List
    {
        public class Query : IRequest<Result<PageList<Unit>>>
        {
            public Guid NotebookId { get; set; }
            public RequestDto RequestDto { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PageList<Unit>>>
        {
            private readonly IUnitRepository _unitRepository;

            public Handler(IUnitRepository unitRepository)
            {
                _unitRepository = unitRepository;
            }
            public async Task<Result<PageList<Unit>>> Handle(Query request, CancellationToken cancellationToken)
            {
                int count = await _unitRepository.GetCountAsync();

                var units = await _unitRepository.GetAllFilteredAsync(request.NotebookId, request.RequestDto);

                var unitsPaged = new PageList<Unit>(units, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Unit>>.Success(unitsPaged);
            }
        }
    }
}
