using Microsoft.EntityFrameworkCore;
using Storage.Core.Exceptions;
using Storage.Core.Interfaces;

namespace Storage.UseCases.Tags.DeleteTag
{
    public class DeleteTagUseCase : IDeleteTagUseCase
    {
        private readonly IFactDbContext _factDbContext;

        public DeleteTagUseCase(IFactDbContext factDbContext)
        {
            _factDbContext = factDbContext;
        }

        public async Task<bool> Execute(Guid id, CancellationToken cancellationToken)
        {
            var tag = await _factDbContext.Tags.FirstOrDefaultAsync(x => x.TagId == id, cancellationToken);
            if (tag is null)
                throw new TagNotFoundException($"Tag with {id} Tag Id not found");
            try
            {
                _factDbContext.Tags.Remove(tag);
                await _factDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
