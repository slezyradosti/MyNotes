using Application.DTOs;
using Application.Models;
using AutoMapper;
using Domain.Models;
using IndentityLogic.Models;
using Profile = Application.Models.Profile;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<Notebook, Notebook>()
                .ForMember(x => x.Units, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore());

            CreateMap<NotebookDto, Notebook>()
                .ForMember(x => x.Units, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore());

            CreateMap<UnitDto, Unit>()
                .ForMember(x => x.Pages, y => y.Ignore())
                .ForMember(x => x.Notebook, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore());

            CreateMap<PageDto, Page>()
                .ForMember(x => x.Notes, y => y.Ignore())
                .ForMember(x => x.Unit, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore());

            CreateMap<NoteDto, Note>()
                .ForMember(x => x.Page, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore());

            CreateMap<ApplicationUser, Profile>();
        }
    }
}
