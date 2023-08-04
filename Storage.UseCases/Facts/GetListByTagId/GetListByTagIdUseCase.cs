using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Exceptions;
using Storage.Core.Interfaces;
using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.GetListByTagId
{
    public class GetListByTagIdUseCase : IGetListByTagIdUseCase
    {
        private readonly IFactDbContext _context;
        private readonly ILogger<GetListByTagIdUseCase> _logger;
        private readonly IMapper _mapper;

        public GetListByTagIdUseCase(IFactDbContext context, 
            ILogger<GetListByTagIdUseCase> logger, 
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FactViewModel>> Execute(Guid tagId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get facts by Tag Id: '{id}'", tagId);
            var tag = await _context.Tags
                .Include(x=>x.Facts)
                .FirstOrDefaultAsync(x => x.TagId == tagId, cancellationToken);
            if (tag == null)
                throw new TagNotFoundException($"Tag with this id not found");

            var facts = _mapper.Map<IEnumerable<FactViewModel>>(tag.Facts);

            return facts;
        }
    }
}
