using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class List
    {
        public class Query : IRequest<Result<List<Note>>>
        {
            public Guid PageId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Note>>>
        {
            private readonly INoteRepository _noteRepository;

            public Handler(INoteRepository noteRepository)
            {
                _noteRepository = noteRepository;
            }

            public async Task<Result<List<Note>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var notes = await _noteRepository.GetAllFromSpecificPageAsync(request.PageId);

                return Result<List<Note>>.Success(notes);
            }
        }
    }
}
