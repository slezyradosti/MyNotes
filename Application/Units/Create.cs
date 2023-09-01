using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Create
    {
        public record Command(UnitDto Unit) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitRepository unitRepository, IMapper mapper,
                IUserAccessor userAccessor)
            {
                _unitRepository = unitRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await _unitRepository.IfUserHasAccessToTheUnits(request.Unit.NotebookId, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var Unit = new Unit();
                _mapper.Map(request.Unit, Unit);
                
                var result = await _unitRepository.AddAsync(Unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to create Unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
