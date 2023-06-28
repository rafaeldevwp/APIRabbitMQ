using System.Text;
using RabbitMQ.Client;

namespace Application.Services
{
    public class CPFProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "CPF_Queue";
        public CPFProducer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",

            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishCPF(string cpf)
        {
            var messagebytes = Encoding.UTF8.GetBytes(cpf);
            _channel.BasicPublish("", QueueName, null, messagebytes);
        }


        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}