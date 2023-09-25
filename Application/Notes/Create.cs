using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Create
    {
        public record Command(NoteDto NoteDto) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly IPageRepository _pageRepository;

            public Handler(INoteRepository noteRepository, IMapper mapper,
                IUserAccessor userAccessor, IPageRepository pageRepository)
            {
                _noteRepository = noteRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _pageRepository = pageRepository;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await _pageRepository.IfUserHasAccessToThePage(request.NoteDto.PageId, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var note = new Note();
                _mapper.Map(request.NoteDto, note);

                var result = await _noteRepository.AddAsync(note) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to create Note");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
