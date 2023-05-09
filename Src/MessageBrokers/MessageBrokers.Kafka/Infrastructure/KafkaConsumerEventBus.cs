using Confluent.Kafka;
using MyEventBus.Base.Interfaces;
using static Confluent.Kafka.ConfigPropertyNames;
using System.Text.Json;
using Confluent.Kafka.Admin;
using System.Threading;

namespace MessageBrokers.Kafka.Infrastructure
{
    internal class KafkaConsumerEventBus : IConsumerBus
    {
        private IConsumer<Null, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        public KafkaConsumerEventBus(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
           _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        }

        
        private string ConsumeStringAsync(CancellationToken cancellationToken = default)
        {
            var result = _consumer.Consume(cancellationToken).Message.Value;
            ArgumentException.ThrowIfNullOrEmpty(result);
            return result;
        }

        
        public T Subscribe<T>(string queueName , CancellationToken cancellationToken = default)
        {
            try
            {
                _consumer.Subscribe(queueName);
                var result = ConsumeStringAsync(cancellationToken);
                var data = JsonSerializer.Deserialize<T>(result);
                _consumer.Commit();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public T Subscribe<T>(string groupName, string queueName , CancellationToken cancellationToken = default)
        {
            try
            {
                _consumer.Close();
                _consumerConfig.GroupId = groupName;
                _consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
                _consumer.Subscribe(queueName);
                var result = ConsumeStringAsync(cancellationToken);
                var data = JsonSerializer.Deserialize<T>(result);
                _consumer.Commit();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

       public string? Subscribe(string topicName , CancellationToken cancellationToken = default)
        {
            _consumer.Subscribe(topicName);
            var data = ConsumeStringAsync(cancellationToken);
            _consumer.Commit();
            return data;
        }

       public string Subscribe(string groupName, string queueName , CancellationToken cancellationToken = default)
        {
            _consumer.Close();
            _consumerConfig.GroupId = groupName;
            _consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
            _consumer.Subscribe(queueName);
            var data =  ConsumeStringAsync(cancellationToken);
            _consumer.Commit();
            return data;
        }  
    }
}
