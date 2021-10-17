using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplePoll.Common.RabbitMq.Configurations;
using SimplePoll.Common.RabbitMq.Providers;
using SimplePoll.Common.RabbitMq.Publishers;
using SimplePoll.Common.RabbitMq.Registration;
using SimplePoll.Common.RabbitMq.Rpc;
using SimplePoll.Common.RabbitMq.Subscribers;

namespace SimplePoll.Common.RabbitMq.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection(nameof(RabbitMqConfiguration)));
            services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>();
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            services.AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>();
            services.AddSingleton<IRpcClient, RpcClient>();
            services.AddSingleton<IRpcConsumer, RpcConsumer>();

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

        public static IServiceCollection RegisterQueue(this IServiceCollection services, string exchangeName, string queueName, string routingKey)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var connectionProvider = serviceProvider.GetRequiredService<IRabbitMqConnectionProvider>();

            var channel = connectionProvider.CreateConnection();

            channel.RegisterQueue(exchangeName, queueName, routingKey);

            return services;
        }
    }
}