using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventBus.Base.Interfaces
{
    public interface IEventBus : ISenderBus , IConsumerBus
    {

    }
}
