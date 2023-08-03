using Storage.Core.ViewModels;

namespace Storage.API.Publisher
{
    public interface INewFactPublisher
    {
        Task Publish(FactViewModel model);
    }
}
