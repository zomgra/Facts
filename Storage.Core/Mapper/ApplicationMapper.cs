using AutoMapper;
using Storage.Core.ViewModels;

namespace Storage.Core.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Tag, TagViewModel>()
                .ForMember(x=>x.Id, opt=>opt.MapFrom(f=>f.TagId));
            CreateMap<Fact, FactViewModel>()
                .ForMember(x=>x.Id, opt=>opt.MapFrom(f=>f.FactId));
        }
    }
}
