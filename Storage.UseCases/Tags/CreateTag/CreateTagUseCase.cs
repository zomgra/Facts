using AutoMapper;
using Common;
using Storage.API.Data;
using Storage.Core;
using Storage.Core.ViewModels;

namespace Storage.UseCases.Tags.CreateTag
{
    public class CreateTagUseCase : ICreateTagUseCase
    {
        private readonly IGuidFactory _guidFactory;
        private readonly IMomentProvider _momentProvider;
        private readonly IFactDbContext _factDbContext;
        private readonly IMapper _mapper;

        public CreateTagUseCase(IGuidFactory guidFactory, IMomentProvider momentProvider, IFactDbContext factDbContext, IMapper mapper)
        {
            _guidFactory = guidFactory;
            _momentProvider = momentProvider;
            _factDbContext = factDbContext;
            _mapper = mapper;
        }

        public async Task<TagViewModel> Excecute(string name, CancellationToken cancellationToken)
        {
            var tag = new Tag
            {
                CreatedAt = _momentProvider.Now,
                Name = name,
                TagId = _guidFactory.CreateGuid(),
            };
            try
            {
                await _factDbContext.Tags.AddAsync(tag);
                await _factDbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<TagViewModel>(tag);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
