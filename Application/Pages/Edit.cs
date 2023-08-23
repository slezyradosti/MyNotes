using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Edit
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public PageDto PageDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPageRepository _pageRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(IPageRepository pageRepository, IMapper mapper,
                IUserAccessor userAccessor)
            {
                _pageRepository = pageRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var page = await _pageRepository.GetOneAsync(request.PageDto.Id);

                if (page == null) return null;

                if (!await _pageRepository.IfUserHasAccessToThePage(request.PageDto.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                _mapper.Map(request.PageDto, page);

                var result = await _pageRepository.SaveAsync(page) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to edit page");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
