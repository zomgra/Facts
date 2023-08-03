
namespace EmailSender.API.Listeners
{
    public class FactEventListener : BackgroundService
    {
        private readonly ILogger<FactEventListener> _logger;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
