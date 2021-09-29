using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;

namespace JetSms.Core.Utilities.MessageQueue.RabbitMqChannelPool
{
    public static class RabbitMqObjectPollDependencyExtensions
    {
        public static IServiceCollection AddRabbitMqChannelPoll(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton(serviceProvider =>
            {
                var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
                var settings = serviceProvider.GetRequiredService<IOptions<RabbitMqSettings>>();
                return provider.Create(new RabbitModelPooledObjectPolicy(settings));
            });

            return services;
        }
    }
}