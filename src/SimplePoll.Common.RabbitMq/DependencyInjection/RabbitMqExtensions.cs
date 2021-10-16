using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.RabbitMq.Configurations;
using SimplePoll.Common.RabbitMq.Factories;
using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Registration;
using SimplePoll.Common.RabbitMq.Rpc;

namespace SimplePoll.Common.RabbitMq.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection(nameof(RabbitMqConfiguration)));
            services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>();
            services.AddSingleton<IRabbitMqPublisherFactory, RabbitMqPublisherFactory>();
            services.AddSingleton<IRabbitMqSubscriberFactory, RabbitMqSubscriberFactory>();
            services.AddSingleton<IRabbitMqRpcClientFactory, RabbitMqRpcClientFactory>();
            services.AddSingleton<IRabbitMqRpcConsumerFactory, RabbitMqRpcConsumerFactory>();
            services.AddSingleton<IRpcServer, RpcServer>();

            return services;
        }

        public static IServiceCollection RegisterExchange(this IServiceCollection services, string exchangeName)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var connectionProvider = serviceProvider.GetRequiredService<IRabbitMqConnectionProvider>();

            var channel = connectionProvider.CreateConnection();
            
            channel.RegisterExchange(exchangeName);

            return services;
        }

        public static IServiceCollection RegisterQueue(this IServiceCollection services, string exchangeName, string queueName)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var connectionProvider = serviceProvider.GetRequiredService<IRabbitMqConnectionProvider>();

            var channel = connectionProvider.CreateConnection();
            
            channel.RegisterQueue(exchangeName, queueName);

            return services;
        }
    }
}