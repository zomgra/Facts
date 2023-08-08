using EmaiSender.Core.ViewModel;

namespace EmailSender.UseCases.Users
{
    public interface ISubscribeToTagUseCase
    {
        Task<bool> Execute(UserViewModel model, Guid tagId, CancellationToken cancellationToken);
    }
}
