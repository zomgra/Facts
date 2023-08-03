using Common;
using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.Models;
using EmaiSender.Core.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.UseCases.Users
{
    public class SubscribeToTagUseCase : ISubscribeToTagUseCase
    {
        private readonly IEmailDbContext _emailDbContext;
        private readonly IGuidFactory _guidFactory;

        public SubscribeToTagUseCase(IEmailDbContext emailDbContext, IGuidFactory guidFactory)
        {
            _emailDbContext = emailDbContext;
            _guidFactory = guidFactory;
        }

        public async Task Execute(UserViewModel model, Guid tagId, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Email = model.Email,
                Name = model.Name,
                UserId = _guidFactory.CreateGuid(),
            };
            try
            {
                var tag = await _emailDbContext.Tags.FirstOrDefaultAsync(x => x.TagId == tagId, cancellationToken);
                await _emailDbContext.Users.AddAsync(newUser);
                newUser.Tags.Add(tag);
                await _emailDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
