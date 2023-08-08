using EmaiSender.Core.ViewModel;

namespace EmailSender.UseCases.Users
{
    public interface IGetSubscribedUsersByTagIdUseCase
    {
        Task<IEnumerable<UserViewModel>> Execute(Guid tagId, CancellationToken cancellationToken);
    }
}
