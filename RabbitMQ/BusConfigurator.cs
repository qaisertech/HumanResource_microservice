using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(IServiceCollection services)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), host =>
                {
                    host.Username(RabbitMqConsts.UserName);
                    host.Password(RabbitMqConsts.Password);
                });
            });
        }
    }
}
