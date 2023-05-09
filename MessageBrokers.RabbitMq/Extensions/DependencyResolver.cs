using MessageBrokers.RabbitMq.Infrasturucture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyEventBus.Base.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokers.RabbitMq.Extensions
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddMyEventRabbitMq(this IServiceCollection services, Action<ConnectionFactory> rabbitMqConfig)
        {
            ConnectionFactory connectionFactory = new();
            rabbitMqConfig.Invoke(connectionFactory);
            ConfiguraRabbitMq(services, connectionFactory);
            return services;
        }

        public static IServiceCollection AddMyEventRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionFactory connectionFactory = configuration.GetSection("RabbitMq").Get<ConnectionFactory>() ??
                throw new ArgumentNullException(typeof(ConnectionFactory).Name);

            ConfiguraRabbitMq(services, connectionFactory);
            return services;
        }

        private static void ConfiguraRabbitMq(IServiceCollection services, ConnectionFactory connectionFactory)
        {
            var conneciton = connectionFactory.CreateConnection();
            services.AddSingleton<IConsumerBus, RabbitMqConsumerEventBus>();
            services.AddSingleton(opt => conneciton);
        }
    }
}
