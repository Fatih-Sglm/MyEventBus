using Microsoft.Extensions.DependencyInjection;
using MyEventBus.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventBus.Base.Extensions
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddMyEventBus<T>(this IServiceCollection services) where T : class  , IEventBus
        {

            return services;
        }
    }
}
