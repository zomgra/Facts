using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Storage.Core.ViewModels;
using System.Text;

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

        public async Task Publish(TagViewModel model)
        {
            _logger.LogInformation("Publish new tag with Id: {Id}", model.Id);

            var channel = _connection.CreateModel();
            var property = channel.CreateBasicProperties();
            var routing = _configuration["RabbitMq:RoutingCreateTagKey"];
            byte[] message = Encoding.ASCII.GetBytes(await JsonConvert.SerializeObjectAsync(model));

            channel.BasicPublish(_configuration["RabbitMq:ExchangeName"], routing, property, message);
        }
    }
}
