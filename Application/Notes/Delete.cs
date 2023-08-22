﻿using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
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
            private readonly IUserAccessor _userAccessor;

            public Handler(INoteRepository noteRepository, IUserAccessor userAccessor)
            {
                _noteRepository = noteRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                //TODO : when you try to delete unexisted data, you also have the following error. Bur or not?)
                if (!await _noteRepository.IfUserHasAccessToTheNote(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var note = await _noteRepository.GetOneAsync(request.Id);

                if (note == null) return null;

                var result = await _noteRepository.RemoveAsync(note) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete note");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
