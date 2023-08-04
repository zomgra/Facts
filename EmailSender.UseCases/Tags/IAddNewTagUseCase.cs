using EmaiSender.Core.ViewModel;

namespace EmailSender.UseCases.Tags
{
    public interface IAddNewTagUseCase
    {
        Task Execute(TagViewModel tagViewModel, CancellationToken cancellationToken);
    }
}
