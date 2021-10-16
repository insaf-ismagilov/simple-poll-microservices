using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePoll.ApiGateway.WebApi.Configurations;
using SimplePoll.Common.Middlewares;
using SimplePoll.Common.RabbitMq;
using SimplePoll.Common.RabbitMq.DependencyInjection;
using SimplePoll.Common.RabbitMq.Endpoints;

namespace SimplePoll.ApiGateway.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(configuration => configuration.ImplicitlyValidateChildProperties = true);

            services
                //.ConfigureAuth(Configuration)
                .ConfigureDi()
                //.ConfigureAutoMapper()
                //.ConfigureValidators()
                .ConfigureSwagger()
                .AddRabbitMq(Configuration);

            services
                .RegisterExchange(RpcEndpoints.PollGetById.Exchange)
                .RegisterQueue(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.RequestQueue, RoutingKeys.Request)
                .RegisterQueue(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.ResponseQueue, RoutingKeys.Response);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorsHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimplePoll.ApiGateway.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}