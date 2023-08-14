using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Edit
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public NotebookDto Notebook { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IMapper _mapper;

            public Handler(INotebookRepository notebookRepository, IMapper mapper)
            {
                _notebookRepository = notebookRepository;
                _mapper = mapper;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var notebook = await _notebookRepository.GetOneAsync(request.Notebook.Id);

                if (notebook == null) return null;

                _mapper.Map(request.Notebook, notebook);

                var result = await _notebookRepository.SaveAsync(notebook) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to update notebook");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
