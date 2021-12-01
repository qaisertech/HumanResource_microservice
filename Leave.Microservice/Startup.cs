using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ;
using Leave.Microservice.Model;
using Leave.Microservice.Repository;
using Leave.Microservice.IRepository;
using Microsoft.EntityFrameworkCore;
using Leave.Microservice.Data;
using Leave.Microservice.Consumer;

namespace Leave.Microservice
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
            services.Configure<LeaveConfiguration>(Configuration.GetSection("LeaveConfiguration"));
            services.AddDbContext<LeaveContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LeaveConnectionString")));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PayrollConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(RabbitMqConsts.Host);
                    cfg.ReceiveEndpoint(RabbitMqConsts.PayrollQueue, c =>
                    {
                        c.ConfigureConsumer<PayrollConsumer>(ctx);
                    });
                });
                //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                // {
                //     config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                //     {
                //         h.Username(RabbitMqConsts.UserName);
                //         h.Password(RabbitMqConsts.Password);
                         
                //     });
                //     config.AutoDelete = false;
                // }));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();

            services.AddScoped<ILeaveRepository, LeaveRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
