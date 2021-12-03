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
using Refit;

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
            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", opt =>
               {
                   opt.RequireHttpsMetadata = false;
                   opt.Authority = "https://localhost:5005";
                   opt.Audience = "employeeApi";
               });
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
            services.AddCors();
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddRefitClient<IEmployeeRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5010/"));
            services.AddScoped<ILeaveRepository, LeaveRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x =>
            {
                x.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
            });
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
