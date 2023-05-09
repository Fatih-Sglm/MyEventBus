using MyEventBus.Base.Interfaces;

namespace MyEventBus.SampleApi.BgService
{
    public class KafkaBackgroundService : BackgroundService
    {
        private readonly ILogger<KafkaBackgroundService> _logger;
        private readonly IConsumerBus _eventBus;

        public KafkaBackgroundService(ILogger<KafkaBackgroundService> logger, IConsumerBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Console.Out.WriteLineAsync("hello world");
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _eventBus.Subscribe<Message>("Fatih","Deneme2", stoppingToken);
                await Console.Out.WriteLineAsync(result.Value);
            }
        }
    }
}
