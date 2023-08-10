using AutoMapper;
using Domain.Models;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Notebook,  Notebook>()
                .ForMember(x => x.Units, y => y.Ignore())
                .ForMember(x => x.Timestamp, y => y.Ignore());
        }
    }
}
