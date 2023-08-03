namespace Storage.UseCases.Tags.DeleteTag
{
    public interface IDeleteTagUseCase
    {
        Task<bool> Execute(Guid id, CancellationToken token);
    }
}
