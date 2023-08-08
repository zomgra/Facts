using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.UseCases.Users
{
    public class GetSubscribedUsersByTagIdUseCase : IGetSubscribedUsersByTagIdUseCase
    {
        private readonly IEmailDbContext _dbContext;

        public GetSubscribedUsersByTagIdUseCase(IEmailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> Execute(Guid tagId, CancellationToken cancellationToken)
        {
            var tag = await _dbContext.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.TagId == tagId, cancellationToken);
            if(tag is null) return Enumerable.Empty<UserViewModel>();

            var users = _dbContext.Users.Select(u=>new UserViewModel
            {
                Email = u.Email,
                Id = u.UserId,
                Name = u.Name
            });
            return users;
        }
    }
}
