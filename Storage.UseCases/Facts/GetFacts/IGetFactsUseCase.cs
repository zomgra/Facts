using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.GetFacts
{
    public interface IGetFactsUseCase
    {
        Task<IEnumerable<FactViewModel>> Execute(int page, string search, CancellationToken cancellationToken);
    }
}
