using AutoMapper;
using Common;
using Microsoft.EntityFrameworkCore;
using Storage.Core;
using Storage.Core.Exceptions;
using Storage.Core.Interfaces;
using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.CreateFact
{
    public class CreateFactsUseCase : ICreateFactUseCase
    {
        private readonly IFactDbContext _factDbContext;
        private readonly IMapper _mapper;
        private readonly IGuidFactory _guidFactory;
        private readonly IMomentProvider _momentProvider;

        public CreateFactsUseCase(IFactDbContext factDbContext, IMapper mapper, IGuidFactory guidFactory, IMomentProvider momentProvider)
        {
            _factDbContext = factDbContext;
            _mapper = mapper;
            _guidFactory = guidFactory;
            _momentProvider = momentProvider;
        }

        public async Task<FactViewModel> Excecute(string content, CancellationToken cancellationToken, params Guid[] tagIds)
        {
            var tags = await _factDbContext.Tags.Where(x=>tagIds.Contains(x.TagId)).ToListAsync(cancellationToken);
            if(tags.Count != tagIds.Length)
                throw new TagNotFoundException("Invalid one or more Tag Id in " + nameof(tagIds));
            if (!tags.Any())
                throw new TagNotFoundException("Not found any Tag in " + nameof(tagIds) + "Tag id");
            try
            {
                var fact = new Fact
                {
                    Content = content,
                    FactId = _guidFactory.CreateGuid(),
                    CreatedAt = _momentProvider.Now,
                    //Tags = tags
                };
                await _factDbContext.Facts.AddAsync(fact);
                fact.Tags = tags ;
                await _factDbContext.SaveChangesAsync(cancellationToken);
                return _mapper.Map<FactViewModel>(fact);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
