using EmaiSender.Core.Models;

namespace EmailSender.UseCases.Facts
{
    public interface ISendMailsToUserByTag
    {
        Task<bool> Execute(Fact fact, CancellationToken cancellationToken);
    }
}
