using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmailSender.UseCases.Tags
{
    public class GetSubscribedUserByNameTagUseCase : IGetSubscribedUserByNameTagUseCase
    {
        private readonly IEmailDbContext _context;
        private readonly ILogger<GetSubscribedUserByNameTagUseCase> _logger;
        public async Task<IEnumerable<UserViewModel>> Execute(string name, CancellationToken cancellationToken)
        {
            name = name.Trim().ToUpper();

            var users = await _context.Users.Where(x => x.Tags.Select(x => x.Name).Contains(name))
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    Id = x.UserId,
                    Name = x.Name
                })
                .ToListAsync();

            _logger.LogInformation("Have {count} users, which subscribe to {tag}", users.Count, name);

            return users;
        }
    }
}
