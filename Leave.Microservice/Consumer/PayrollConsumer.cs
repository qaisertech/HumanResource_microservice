using Leave.Microservice.IRepository;
using MassTransit;
using RabbitMQ.Models;
using System.Threading.Tasks;

namespace Leave.Microservice.Consumer
{
    public class PayrollConsumer : IConsumer<PayrollLedgerModel>
    {
        private readonly ILeaveRepository _leaveRepository;

        public PayrollConsumer(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }
        public Task Consume(ConsumeContext<PayrollLedgerModel> context)
        {
            _leaveRepository.UpdateLeave(context.Message);
            return Task.CompletedTask;
        }
    }
}
