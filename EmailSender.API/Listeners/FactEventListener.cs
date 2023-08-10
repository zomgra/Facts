using EmailSender.UseCases.Facts;
using EmaiSender.Core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailSender.API.Listeners
{
    public class FactEventListener : BackgroundService
    {
        private readonly ILogger<FactEventListener> _logger;
        private readonly IConnection _connection;
        private readonly ISendMailsToUserByTag _useCase;
        private readonly IConfiguration _configuration;

        public FactEventListener(ILogger<FactEventListener> logger,
            IConnection connection,
            ISendMailsToUserByTag useCase,
            IConfiguration configuration)
        {
            _logger = logger;
            _connection = connection;
            _useCase = useCase;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _logger.LogInformation("Fact Event Listener is start");

            var channel = _connection.CreateModel();
            var customer = new AsyncEventingBasicConsumer(channel);

            customer.Received += async (sender, e) =>
            {
                var json = Encoding.ASCII.GetString(e.Body.ToArray());
                var fact = JsonConvert.DeserializeObject<Fact>(json);

                _logger.LogInformation("Received new fact from queue with id: {id}", fact.Id);

                var result = await _useCase.Execute(fact, stoppingToken);
                channel.BasicAck(e.DeliveryTag, false);
            };
            channel.BasicConsume(_configuration["RabbitMq:FactQueue"], false, customer);
            //throw new NotImplementedException();
        }
    }
}
