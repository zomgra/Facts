using Storage.Core.ViewModels;

namespace Storage.UseCases.Facts.GetListByTagId
{
    public interface IGetListByTagIdUseCase
    {
        Task<IEnumerable<FactViewModel>> Execute(Guid tagId, CancellationToken cancellationToken);
    }
}
