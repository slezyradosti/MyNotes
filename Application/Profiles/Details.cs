using Application.Core;
using Application.Models;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Profile = Application.Models.Profile;

namespace Application.Profiles
{
    public class Details
    {
        public record Query(Guid userId) : IRequest<Result<Profile>>;

        public class Handler : IRequestHandler<Query, Result<Profile>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(IUserRepository userRepository, IUserAccessor userAccessor,
                IMapper mapper)
            {
                _userRepository = userRepository;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUser(request.userId);

                var profile = new Profile();
                _mapper.Map(user, profile);

                return Result<Profile>.Success(profile);
            }
        }
    }
}
