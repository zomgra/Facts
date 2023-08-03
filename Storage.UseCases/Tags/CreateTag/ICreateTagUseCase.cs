using Storage.Core.ViewModels;

namespace Storage.UseCases.Tags.CreateTag
{
    public interface ICreateTagUseCase
    {
        Task<TagViewModel> Execute(string name, CancellationToken cancellationToken);
    }
}
