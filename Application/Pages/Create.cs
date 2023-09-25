using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Pages
{
    public class Create
    {
        public record Command(PageDto pageDto) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPageRepository _pageRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitRepository _unitRepository;

            public Handler(IPageRepository pageRepository, IMapper mapper,
                IUserAccessor userAccessor, IUnitRepository unitRepository)
            {
                _pageRepository = pageRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
                _unitRepository = unitRepository;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await _unitRepository.IfUserHasAccessToTheUnit(request.pageDto.UnitId, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var page = new Page();
                _mapper.Map(request.pageDto, page);

                var result = await _pageRepository.AddAsync(page) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to create Page");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
