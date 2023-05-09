using MyEventBus.Base.Interfaces;

namespace MyEventBus.SampleApi.BgService
{
    public class RabbitMqBackgroundService : BackgroundService
    {
        private readonly IConsumerBus _consumerBus;

        public RabbitMqBackgroundService(IConsumerBus consumerBus)
        {
            _consumerBus = consumerBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = _consumerBus.Subscribe("DenemeRabbit", stoppingToken);
                var data2 = _consumerBus.Subscribe("DenemeRabbit3", stoppingToken);
                await Console.Out.WriteLineAsync(data);
                //if (!string.IsNullOrEmpty(data))
                //    await Console.Out.WriteLineAsync(data);

                //if(!string.IsNullOrEmpty(data2))
                //    await Console.Out.WriteLineAsync(data2);
            }
        }
    }
}
