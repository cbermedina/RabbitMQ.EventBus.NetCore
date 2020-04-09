using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RabbitMQ.EventBus.NetCore.Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRabbitMQEventBus("amqp://xpsxyjse:nqXwuQgoRcJCp7yPMvRqSGLWEh7HEWwX@barnacle.rmq.cloudamqp.com/xpsxyjse", eventBusOptionAction: eventBusOption =>
            {
                eventBusOption.ClientProvidedAssembly<Startup>();
                eventBusOption.EnableRetryOnFailure(true, 5000, TimeSpan.FromSeconds(30));
                eventBusOption.RetryOnFailure(TimeSpan.FromSeconds(1));
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.RabbitMQEventBusAutoSubscribe();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
