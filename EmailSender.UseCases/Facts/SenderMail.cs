using EmaiSender.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmailSender.UseCases.Facts
{
    public class SenderMail : ISenderMail
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SenderMail> _logger;
       
        public SenderMail(
            IConfiguration configuration,
            ILogger<SenderMail> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(Fact fact, User user, Tag tag)
        {
            _logger.LogInformation("Sending to {user} new fact: {fact}", user.Name, fact.Content);
            await Task.Delay(2300);
        }
    }
}
