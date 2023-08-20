using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class List
    {
        public class Query : IRequest<Result<PageList<Note>>>
        {
            public Guid PageId { get; set; }
            public RequestDto RequestDto { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PageList<Note>>>
        {
            private readonly INoteRepository _noteRepository;

            public Handler(INoteRepository noteRepository)
            {
                _noteRepository = noteRepository;
            }

            public async Task<Result<PageList<Note>>> Handle(Query request, CancellationToken cancellationToken)
            {
                int count = await _noteRepository.GetCountAsync();

                var notes = await _noteRepository.GetAllFilteredAsync(request.PageId, request.RequestDto);

                var notesPaged = new PageList<Note>(notes, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Note>>.Success(notesPaged);
            }
        }
    }
}
