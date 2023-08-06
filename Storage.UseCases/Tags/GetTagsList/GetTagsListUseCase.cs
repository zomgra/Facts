using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Interfaces;
using Storage.Core.ViewModels;

namespace Storage.UseCases.Tags.GetTagsList
{
    public class GetTagsListUseCase : IGetTagsListUseCase
    {
        private readonly IMapper _mapper;
        private readonly IFactDbContext _context;

        public GetTagsListUseCase(IMapper mapper, 
            IFactDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TagViewModel>> Execute(int page, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.Skip((page - 1) * 5).Take(5).ToListAsync(cancellationToken);
            var viewModels = _mapper.Map<List<TagViewModel>>(tags);
            return viewModels;
        }
    }
}
