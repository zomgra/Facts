using Newtonsoft.Json;
using RabbitMQ.Client;
using Storage.Core.ViewModels;
using System.Text;

namespace Storage.API.Publisher
{
    public class NewFactPublisher : INewFactPublisher
    {
        private readonly IConnection _connection;
        private readonly ILogger<NewFactPublisher> _logger;
        private readonly IConfiguration _configuration;

        public NewFactPublisher(IConnection connection,
            ILogger<NewFactPublisher> logger,
            IConfiguration configuration)
        {
            _connection = connection;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Publish(FactViewModel model)
        {
            _logger.LogInformation("Start publish new Fact with Id: {0}", model.Id);
            var channel = _connection.CreateModel();

            var excangeName = _configuration["RabbitMq:ExchangeName"];
            var routingKey = _configuration["RabbitMq:RoutingCreateKey"];

            var property = channel.CreateBasicProperties();

            var json = await JsonConvert.SerializeObjectAsync(model);
            var message = Encoding.ASCII.GetBytes(json);

            channel.BasicPublish(excangeName, routingKey, property, message);
            _logger.LogInformation("End publish new Fact with Id: {0}", model.Id);
        }
    }
}
