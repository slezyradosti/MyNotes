﻿using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Create
    {
        public record Command(NotebookDto NotebookDto) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(INotebookRepository notebookRepository, IMapper mapper,
                IUserAccessor userAccessor)
            {
                _notebookRepository = notebookRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.NotebookDto.UserId = _userAccessor.GetUserId();

                var notebook = new Notebook();
                _mapper.Map(request.NotebookDto, notebook);

                var result = await _notebookRepository.AddAsync(notebook) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to create notebook");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
