using Storage.Core.ViewModels;

namespace Storage.UseCases.Tags.GetTagsList
{
    public interface IGetTagsListUseCase
    {
        Task<IEnumerable<TagViewModel>> Execute(int page, CancellationToken cancellationToken);
    }
}
