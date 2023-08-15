using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Create
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public PageDto pageDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPageRepository _pageRepository;
            private readonly IMapper _mapper;

            public Handler(IPageRepository pageRepository, IMapper mapper)
            {
                _pageRepository = pageRepository;
                _mapper = mapper;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var page = new Page();
                _mapper.Map(request.pageDto, page);

                var result = await _pageRepository.AddAsync(page) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to create Page");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
