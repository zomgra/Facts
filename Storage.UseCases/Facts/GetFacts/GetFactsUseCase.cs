using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Exceptions;
using Storage.Core.Interfaces;
using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.GetFacts
{
    public class GetFactsUseCase : IGetFactsUseCase
    {
        private readonly IFactDbContext _factDbContext;
        private readonly IMapper _mapper;

        public GetFactsUseCase(IFactDbContext factDbContext, IMapper mapper)
        {
            _factDbContext = factDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FactViewModel>> Execute(int page, string search, CancellationToken cancellationToken)
        {
            try
            {
                var facts = _factDbContext.Facts.Include(x=>x.Tags).AsNoTracking().Skip((page - 1) * 5).Take(5);
                if (!facts.Any())
                {
                    throw new FactNotFoundException("No found any facts");
                }
                if (!string.IsNullOrWhiteSpace(search))
                {
                    facts = facts.Where(x => x.Content.Contains(search));
                }
                return _mapper.Map<List<FactViewModel>>(await facts.ToListAsync(cancellationToken));

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
