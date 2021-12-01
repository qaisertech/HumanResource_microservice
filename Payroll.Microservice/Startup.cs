using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payroll.Microservice.Consumer;
using Payroll.Microservice.Data;
using Payroll.Microservice.IRepository;
using Payroll.Microservice.Model;
using Payroll.Microservice.Repository;
using RabbitMQ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payroll.Microservice
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
            services.Configure<PayrollConfiguration>(Configuration.GetSection("PayrollConfiguration"));

            services.AddDbContext<PayrollContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PayrollConnectionString")));
            services.AddMassTransit(x =>
            {
               
                x.AddConsumer<LeaveConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(RabbitMqConsts.Host);
                    cfg.ReceiveEndpoint(RabbitMqConsts.LeaveQueue, c =>
                    {
                        c.UseTransaction(y =>
                        {
                            y.Timeout = TimeSpan.FromSeconds(90);
                            y.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                        });
                        c.ConfigureConsumer<LeaveConsumer>(context);
                    });
                });
                //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                //{
                //    config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                //    {
                //        h.Username(RabbitMqConsts.UserName);
                //        h.Password(RabbitMqConsts.Password);

                //    });
                //    config.AutoDelete = false;
                //    //config.ReceiveEndpoint("Leave_Queue", e =>
                //    //{
                //    //    e.PrefetchCount = 16;
                //    //    e.UseMessageRetry(r => r.Interval(2, 100));
                //    //    e.Consumer<LeaveConsumer>();
                //    //});
                //}));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            //services.AddHostedService<LeaveConsumerHostedService>();
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
