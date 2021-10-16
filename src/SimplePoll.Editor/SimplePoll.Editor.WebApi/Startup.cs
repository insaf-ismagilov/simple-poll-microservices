using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePoll.Common.Authentication.Extensions;
using SimplePoll.Common.Middlewares;
using SimplePoll.Common.RabbitMq;
using SimplePoll.Common.RabbitMq.DependencyInjection;
using SimplePoll.Common.RabbitMq.Endpoints;
using SimplePoll.Editor.Configurations;
using SimplePoll.Editor.Rpc;

namespace SimplePoll.Editor
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddFluentValidation(configuration => configuration.ImplicitlyValidateChildProperties = true);

			services
				.ConfigureAuth(Configuration)
				.ConfigureDb(Configuration)
				.ConfigureDi()
				.ConfigureAutoMapper()
				.ConfigureValidators()
				.ConfigureSwagger()
				.AddRabbitMq(Configuration);

			services
				.RegisterExchange(RpcEndpoints.PollGetById.Exchange)
				.RegisterQueue(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.RequestQueue, RoutingKeys.Request)
				.RegisterQueue(RpcEndpoints.PollGetById.Exchange, RpcEndpoints.PollGetById.ResponseQueue, RoutingKeys.Response);

			services.AddHostedService<RpcServerHostedService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ErrorsHandlerMiddleware>();
			
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimplePoll.Editor.WebApi v1"));
			}

			// app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}