
namespace EmailSender.API.Listeners
{
    public class FactEventListener : BackgroundService
    {
        private readonly ILogger<FactEventListener> _logger;

        public FactEventListener(ILogger<FactEventListener> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _logger.LogInformation("Fact Event Listener is start");
            //throw new NotImplementedException();
        }
    }
}
