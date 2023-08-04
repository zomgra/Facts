
using EmaiSender.Core.ViewModel;

namespace EmailSender.UseCases.Tags
{
    public interface IGetSubscribedUserByNameTagUseCase
    {
        Task<IEnumerable<UserViewModel>> Execute(string Name, CancellationToken cancellationToken);
    }
}
