using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.CreateFact
{
    public interface ICreateFactUseCase
    {
        Task<FactViewModel> Excecute(string context, CancellationToken cancellationToken, params Guid[] tagIds);
    }
}
