using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventBus.Base.Interfaces
{
    public interface IConsumerBus
    {
        T Subscribe<T>(string queueName , CancellationToken cancellationToken = default);
        T Subscribe<T>(string topicName, string queueName , CancellationToken cancellationToken = default);
        string Subscribe(string topicName , CancellationToken cancellationToken = default);
        string Subscribe(string topicName , string queueName , CancellationToken cancellationToken = default);
    }
}
