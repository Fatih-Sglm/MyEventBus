using Confluent.Kafka;
using MessageBrokers.Kafka.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MyEventBus.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokers.Kafka.Extensions
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddMyEventBusKafka(this IServiceCollection services , Action<ConsumerConfig> kafkaConfig)
        {
            var consumerConfig = new ConsumerConfig();
            kafkaConfig.Invoke(consumerConfig);
            services.AddSingleton(opt => consumerConfig);
            services.AddSingleton<IConsumerBus, KafkaConsumerEventBus>();
            return services;
        }
    }
}
