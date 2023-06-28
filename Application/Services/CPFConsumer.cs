using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Application.Services
{
    public class CPFConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "CPF_Queue";


        public CPFConsumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void ConsumerCPF()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                Console.WriteLine($"CPF Recebido:{message}");
            };

            _channel.BasicConsume(QueueName, true, consumer);
        }


        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

    }
}