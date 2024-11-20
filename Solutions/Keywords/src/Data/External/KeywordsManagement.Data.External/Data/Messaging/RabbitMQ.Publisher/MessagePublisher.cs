namespace KeywordsManagement.Data.External.Messaging;

using Cloudio.Core.Services.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public class MessagePublisher : IMessagePublisher
{
    private readonly MessageQueueConfigs _options;

    private IConnection _connection = null!;
    private IModel _model = null!;

    public MessagePublisher(IOptionsMonitor<MessageQueueConfigs> options)
    {
        _options = options.CurrentValue;
        Initialize();
    }
    void Initialize()
    {
        CreateConnection();
        CreateModel();
        CreateExchange();
    }

    public void Publish(byte[] messageBytes)
    => _model.BasicPublish(_options.ExchangeName, _options.RouteKey, null, messageBytes);

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

    #endregion
}