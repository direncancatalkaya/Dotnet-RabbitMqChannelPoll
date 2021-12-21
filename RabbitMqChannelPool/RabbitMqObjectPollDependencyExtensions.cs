using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;

namespace Utilities.MessageQueue.RabbitMqChannelPool
{
    public static class RabbitMqObjectPollDependencyExtensions
    {
        public static IServiceCollection AddRabbitMqChannelPoll(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<RabbitMqSettings>>();
                return new DefaultObjectPoolProvider().Create(new RabbitModelPooledObjectPolicy(settings));
            });

            return services;
        }
    }
}