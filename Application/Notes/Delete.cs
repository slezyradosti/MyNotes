using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Delete
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INoteRepository _noteRepository;

            public Handler(INoteRepository noteRepository)
            {
                _noteRepository = noteRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var note = await _noteRepository.GetOneAsync(request.Id);

                if (note == null) return null;

                var result = await _noteRepository.RemoveAsync(note) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete note");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
