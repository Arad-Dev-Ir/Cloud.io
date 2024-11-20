namespace NewsManagement.Data.External.Messaging;

using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Cloudio.Core.Services.Messaging;

public class MessageConsumer : IMessageConsumer
{
    private readonly MessageQueueConfigs _options;

    private IConnection _connection = null!;
    private IModel _model = null!;
    private string _queue = null!;

    public MessageConsumer(IOptionsMonitor<MessageQueueConfigs> options)
    {
        _options = options.CurrentValue;
        Initialize();
    }
    void Initialize()
    {
        CreateConnection();
        CreateModel();
        CreateExchange();
        CreateQueue();
        BindQueue();
    }

    public void Consume(Action<BasicDeliveryEventArgs> act)
    {
        var consumer = CreateEventBaseConsumer();
        BasicDeliveryEventArgs eventArgs = new();
        consumer.Received += (sender, args) => act(new BasicDeliveryEventArgs { Body = args.Body });
        _model.BasicConsume(_queue, false, consumer);
    }

    public void Acknowledge(ulong deliveryTag, bool multiple)
    => _model.BasicAck(deliveryTag, multiple);

    public void Close()
    => _connection?.Close();

    public void Dispose()
    => _connection?.Dispose();

    #region Private Methods

    private void CreateConnection()
    => _connection = new ConnectionFactory { HostName = _options.HostName }.CreateConnection();

    private void CreateModel()
    => _model = _connection.CreateModel();

    private void CreateExchange()
    => _model.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType, false, true, null);

    private void CreateQueue()
    => _queue = _model.QueueDeclare().QueueName;

    private void BindQueue()
    => _model.QueueBind(_queue, _options.ExchangeName, _options.RouteKey, null);

    private EventingBasicConsumer CreateEventBaseConsumer()
    => new(_model);

    #endregion
}