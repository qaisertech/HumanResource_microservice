using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Payroll.Microservice.Model;
using RabbitMQ;
using RabbitMQ.Client.Events;
using RabbitMQ.Models;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payroll.Microservice.Consumer
{
    public class LeaveConsumerHostedService : IHostedService, IDisposable
    {
        private readonly PayrollConfiguration _configuration;
        private Timer _timer;
        public LeaveConsumerHostedService(IOptions<PayrollConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            
            _timer = new Timer(Consume, null, TimeSpan.Zero,
             TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected void Consume(Object state)
        {
            LeaveConsumer leaveConsumer = new LeaveConsumer();
            //var channel = RabbitMQConnection.ConnectToRabbitMQ(_configuration.LeaveQueue, "leave.init");

            //var consumer = new EventingBasicConsumer(channel);

            //consumer.Received += (sender, ea) =>
            //{
            //    var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            //    System.Diagnostics.Debug.WriteLine(" [x] {0}", content);
            //    var model = JsonConvert.DeserializeObject<LeaveConsumerModel>(content);
            //};


            //using (var scope = _services.CreateScope())
            //{
            //    var scopedProcessingService =
            //        scope.ServiceProvider
            //            .GetRequiredService<IScopedAlertingService>();

            //    await scopedProcessingService.DoWork(stoppingToken);
            //}
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
    public class LeaveConsumer : IConsumer<LeaveConsumerModel>
    {
        public Task Consume(ConsumeContext<LeaveConsumerModel> context)
        {
            var t = context.Message;
            return Task.CompletedTask;
        }
    }
}
