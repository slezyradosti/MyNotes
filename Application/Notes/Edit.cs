using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Edit
    {
        public record Command(NoteDto NoteDto) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(INoteRepository noteRepository, IMapper mapper, 
                IUserAccessor userAccessor)
            {
                _noteRepository = noteRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var note = await _noteRepository.GetOneAsync(request.NoteDto.Id);

                if (note == null) return null;

                if (!await _noteRepository.IfUserHasAccessToTheNote(request.NoteDto.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                _mapper.Map(request.NoteDto, note);

                var result = await _noteRepository.SaveAsync(note) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to edit note");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
