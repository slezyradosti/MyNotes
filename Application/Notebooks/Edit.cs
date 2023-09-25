using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Edit
    {
        public record Command(NotebookDto Notebook) : IRequest<Result<MediatR.Unit>>;

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
                var notebook = await _notebookRepository.GetOneAsync(request.Notebook.Id);

                if (notebook == null) return null;

                request.Notebook.UserId = _userAccessor.GetUserId();

                if (!await _notebookRepository.IfUserHasAccessToTheNotebook(request.Notebook.Id,
                    _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                _mapper.Map(request.Notebook, notebook);

                var result = await _notebookRepository.SaveAsync(notebook) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to update notebook");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
