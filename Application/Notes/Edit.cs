using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Edit
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public NoteDto NoteDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IMapper _mapper;

            public Handler(INoteRepository noteRepository, IMapper mapper)
            {
                _noteRepository = noteRepository;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var note = await _noteRepository.GetOneAsync(request.NoteDto.Id);

                if (note == null) return null;

                _mapper.Map(request.NoteDto, note);

                var result = await _noteRepository.SaveAsync(note) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to edit note");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
