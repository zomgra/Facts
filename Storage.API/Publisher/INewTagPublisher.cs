using Storage.Core.ViewModels;

namespace Storage.API.Publisher
{
    public interface INewTagPublisher
    {
        Task Publish(TagViewModel model);
    }
}
