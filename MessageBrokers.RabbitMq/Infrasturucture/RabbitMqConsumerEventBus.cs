using MyEventBus.Base.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MessageBrokers.RabbitMq.Infrasturucture
{
    public class RabbitMqConsumerEventBus : IConsumerBus
    {
        private readonly IModel _model;

        public RabbitMqConsumerEventBus(IConnection connection)
        {
            _model ??= connection.CreateModel();
        }
        public T Subscribe<T>(string exchangeName, string queueName , CancellationToken cancellationToken = default)
        {
            _model.ExchangeDeclare(exchange: exchangeName, type: "direct");
            _model.QueueDeclare(queueName, true, false, false, null);
            _model.QueueBind(queueName, exchangeName, queueName);

            var result = ConsumeMessageString(queueName);
            if (string.IsNullOrWhiteSpace(result))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(result);

        }

        public string Subscribe(string topicName , CancellationToken cancellationToken = default)
        {
           return Subscribe("Default", topicName, cancellationToken);
        }

       public string Subscribe(string exchangeName, string queueName , CancellationToken cancellationToken = default)
        {
            _model.ExchangeDeclare(exchange: exchangeName, type: "direct");
            _model.QueueDeclare(queueName, true, false, false, null);
            _model.QueueBind(queueName, exchangeName, queueName);

            return ConsumeMessageString(queueName);
        }

        public T Subscribe<T>(string queueName , CancellationToken cancellationToken = default)
        {
            var data =  Subscribe("Default", queueName, cancellationToken);
            if (string.IsNullOrWhiteSpace(data))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        private string? ConsumeMessageString(string queueName)
        {
            var message = _model.BasicGet(queueName, autoAck: true);

            while (message == null)
            {
                Thread.Sleep(100); 
                message = _model.BasicGet(queueName, autoAck: true);
            }
            return Encoding.UTF8.GetString(message?.Body.ToArray());
        }
    }
}
