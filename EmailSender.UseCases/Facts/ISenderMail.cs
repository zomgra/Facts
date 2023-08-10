using EmaiSender.Core.Models;

namespace EmailSender.UseCases.Facts
{
    public interface ISenderMail
    {
        Task SendEmailAsync(Fact fact, User user, Tag tag);
    }
}