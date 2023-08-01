using Storage.Core.ViewModels;

namespace Storage.UseCases.Tags.CreateTag
{
    public interface ICreateTagUseCase
    {
        Task<TagViewModel> Excecute(string name, CancellationToken cancellationToken);
    }
}
