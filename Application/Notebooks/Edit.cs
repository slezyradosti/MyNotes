using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Edit
    {
        public class Command : IRequest<MediatR.Unit>
        {
            public Notebook Notebook { get; set; }
        }

        public class Handler : IRequestHandler<Command, MediatR.Unit>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IMapper _mapper;

            public Handler(INotebookRepository notebookRepository, IMapper mapper)
            {
                _notebookRepository = notebookRepository;
                _mapper = mapper;
            }

            public async Task<MediatR.Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var notebook = await _notebookRepository.GetOneAsync(request.Notebook.Id);

                _mapper.Map(request.Notebook, notebook);

                await _notebookRepository.SaveAsync(notebook);

                return MediatR.Unit.Value;
            }
        }
    }
}
