using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Delete
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPageRepository _pageRepository;

            public Handler(IPageRepository pageRepository)
            {
                _pageRepository = pageRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var page = await _pageRepository.GetOneAsync(request.Id);

                if (page == null) return null;

                var result = await _pageRepository.RemoveAsync(page) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete page");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
