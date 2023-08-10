namespace EmailSender.UseCases.Users
{
    public interface IUnSubscribeToTagUseCase
    {
        Task Execute(Guid tagId, string userId, CancellationToken cancellationToken);
    }
}
