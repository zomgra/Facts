using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmailSender.UseCases.Facts
{
    public class SendMailsToUserByTag : ISendMailsToUserByTag
    {
        private readonly IEmailDbContext _context;
        private readonly ILogger<SendMailsToUserByTag> _logger;
        private readonly ISenderMail _senderMail;

        public SendMailsToUserByTag(IEmailDbContext context,
            ILogger<SendMailsToUserByTag> logger,
            ISenderMail senderMail)
        {
            _context = context;
            _logger = logger;
            _senderMail = senderMail;
        }

        public async Task<bool> Execute(Fact fact, CancellationToken cancellationToken)
        {
            var tagIds = fact.Tags.Select(factTag => factTag.TagId).ToList();

            var users = await _context.Users
                .Include(u => u.Tags)
                .Where(u => u.Tags.Any(tag => tagIds.Contains(tag.TagId)))
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Send mails to users new fact: {content}", fact.Content);

            foreach (var tag in fact.Tags)
            {
                foreach (var user in users)
                {
                    if (user.Tags.Any(utag => utag.TagId == tag.TagId))
                        await _senderMail.SendEmailAsync(fact, user, tag);
                }
            }
            return true;
        }
    }
}
