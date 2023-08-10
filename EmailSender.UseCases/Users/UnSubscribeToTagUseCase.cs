using EmaiSender.Core.Exceptions;
using EmaiSender.Core.Inderfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmailSender.UseCases.Users
{
    public class UnSubscribeToTagUseCase : IUnSubscribeToTagUseCase
    {
        private readonly IEmailDbContext _emailDbContext;
        private readonly ILogger<UnSubscribeToTagUseCase> _logger;

        public UnSubscribeToTagUseCase(IEmailDbContext emailDbContext,
            ILogger<UnSubscribeToTagUseCase> logger)
        {
            _emailDbContext = emailDbContext;
            _logger = logger;
        }

        public async Task Execute(Guid tagId, string userId, CancellationToken cancellationToken)
        {
            try
            {
                var tag = await _emailDbContext.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.TagId == tagId, cancellationToken);
                if (tag is null)
                    throw new TagNotExistsException($"Tag not exists with id: {tagId}");
                var user = await _emailDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId), cancellationToken);
                if (user is null)
                {
                    return;
                }
                var userExistsTag = user.Tags.Contains(tag);
                if (!userExistsTag)
                {
                    return;
                }
                user.Tags.Remove(tag);
                await _emailDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error with unsub: {error}", ex.Message);
            }

        }
    }
}
