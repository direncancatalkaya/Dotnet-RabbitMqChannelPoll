using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace JetSms.Core.Utilities.MessageQueue.RabbitMqChannelPool
{
    public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly IConnection _connection;
        private readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitModelPooledObjectPolicy(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _connection = GetConnection();
        }

        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen) return true;

            obj?.Dispose();
            return false;
        }

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                UserName = _rabbitMqSettings.UserName,
                Password = _rabbitMqSettings.Password,
                Port = _rabbitMqSettings.Port,
                VirtualHost = _rabbitMqSettings.VirtualHost
            };

            return factory.CreateConnection();
        }
    }
}