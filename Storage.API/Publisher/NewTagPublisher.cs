using RabbitMQ.Client;
using Storage.Core.ViewModels;

namespace Storage.API.Publisher
{
    public class NewTagPublisher : INewTagPublisher
    {
        private readonly IConnection _connection;
        private readonly ILogger<NewTagPublisher> _logger;
        private readonly IConfiguration _configuration;

        public NewTagPublisher(IConnection connection,
            ILogger<NewTagPublisher> logger,
            IConfiguration configuration)
        {
            _connection = connection;
            _logger = logger;
            _configuration = configuration;
        }

        public Task Publish(TagViewModel model)
        {
            _logger.LogInformation("Publish new tag with Id: {0}", model.Id);
            throw new NotImplementedException();
        }
    }
}
