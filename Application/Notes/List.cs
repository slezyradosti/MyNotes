using Application.Core;
using Application.DTOs;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class List
    {
        public record Query(Guid PageId, RequestDto RequestDto) : IRequest<Result<PageList<Note>>>;

        public class Handler : IRequestHandler<Query, Result<PageList<Note>>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IUserAccessor _userAccessor;
            private readonly IPageRepository _pageRepository;

            public Handler(INoteRepository noteRepository, IUserAccessor userAccessor,
                IPageRepository pageRepository)
            {
                _noteRepository = noteRepository;
                _userAccessor = userAccessor;
                _pageRepository = pageRepository;
            }

            public async Task<Result<PageList<Note>>> Handle(Query request, CancellationToken cancellationToken)
            {
                

                //if (count > 0)
                //{
                //    if (!await _noteRepository.IfUserHasAccessToTheNotes(request.PageId, _userAccessor.GetUserId()))
                //    {
                //        return Result<PageList<Note>>.Failure("You have no access to this data");
                //    }
                //}
                //else
                {
                    if (!await _pageRepository.IfUserHasAccessToThePage(request.PageId, _userAccessor.GetUserId()))
                    {
                        return Result<PageList<Note>>.Failure("You have no access to this data");
                    }
                }

                int count = await _noteRepository.GetOwnedCountAsync(request.PageId);

                var notes = await _noteRepository.GetAllFilteredAsync(request.PageId, request.RequestDto);

                var notesPaged = new PageList<Note>(notes, request.RequestDto.PageIndex,
                    request.RequestDto.PageSize, count);

                return Result<PageList<Note>>.Success(notesPaged);
            }
        }
    }
}
