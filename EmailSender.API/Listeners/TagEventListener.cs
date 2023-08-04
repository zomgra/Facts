using EmailSender.UseCases.Tags;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using EmaiSender.Core.ViewModel;

namespace EmailSender.API.Listeners
{
    public class TagEventListener : BackgroundService
    {
        private readonly ILogger<TagEventListener> _logger;
        private readonly IConnection _connection;
        private readonly IAddNewTagUseCase _useCase;
        private readonly IConfiguration _configuration;

        public TagEventListener(ILogger<TagEventListener> logger,
            IConnection connection,
            IAddNewTagUseCase useCase,
            IConfiguration configuration)
        {
            _logger = logger;
            _connection = connection;
            _useCase = useCase;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();
            var customer = new AsyncEventingBasicConsumer(channel);

            var queueName = _configuration["RabbitMq:TagQueue"]; 
            //var exName = _configuration["RabbitMq:FactExchange"];

            customer.Received += async (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var tag = JsonConvert.DeserializeObject<TagViewModel>(json);

                await _useCase.Execute(tag, stoppingToken);
                _logger.LogInformation("Add new tag with name: {name}", tag.Name);
                channel.BasicAck(args.DeliveryTag, false);
            };
            channel.BasicConsume(queueName, false, customer);

            _logger.LogInformation("Starting and add handler to AddingNewTag");
        }

    }
}
