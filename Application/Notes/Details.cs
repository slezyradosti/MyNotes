using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Details
    {
        public class Query : IRequest<Result<Note>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Note>>
        {
            private readonly INoteRepository _noteRepository;

            public Handler(INoteRepository noteRepository)
            {
                _noteRepository = noteRepository;
            }

            public async Task<Result<Note>> Handle(Query request, CancellationToken cancellationToken)
            {
                var note = await _noteRepository.GetOneAsync(request.Id);

                return Result<Note>.Success(note);
            }
        }
    }
}
