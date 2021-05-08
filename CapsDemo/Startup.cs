using CapsDemo.ServiceOne.Shared;
using DotNetCore.CAP;
using DotNetCore.CAP.Messages;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Savorboard.CAP.InMemoryMessageQueue;
using ServiceBus.Distributed.DependencyInjection;
using System.Reflection;

namespace CapsDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();
                //x.UseRabbitMQ("localhost");
                x.UseInMemoryMessageQueue();
                x.UseDashboard();
                x.FailedThresholdCallback = failed =>
                {
                    var logger = failed.ServiceProvider.GetService<ILogger<Startup>>();
                    logger.LogError($@"A message of type {failed.MessageType} failed after executing {x.FailedRetryCount} several times, 
                        requiring manual troubleshooting. Message name: {failed.Message.GetName()}");
                };
            });

            var azureSbConnectionString = Configuration.GetConnectionString("AzureServiceBusConnection");

            services.AddServiceBus(new ServiceBusOptions(azureSbConnectionString, "caps.demo"))
                .EnsureEventsFromAssemblyContaining<Startup>();

            services.AddTransientTypesImplementing<ICapSubscribe>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CapsDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CapsDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCapDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
