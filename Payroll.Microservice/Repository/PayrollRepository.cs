using MassTransit;
using Payroll.Microservice.Data;
using Payroll.Microservice.IRepository;
using Payroll.Microservice.Model;
using RabbitMQ.Models;
using System.Threading.Tasks;

namespace Payroll.Microservice.Repository
{
    public class PayrollRepository: IPayrollRepository
    {
        private readonly PayrollContext _payrollContext;
        private IPublishEndpoint _publishEndpoint;

        public PayrollRepository(PayrollContext payrollContext, IPublishEndpoint publishEndpoint)
        {
            _payrollContext = payrollContext;
            _publishEndpoint = publishEndpoint;
        }
        public void AddPayroll(PayrollModel model)
        {
            _payrollContext.Payrolls.Add(model);
            _payrollContext.SaveChanges();
        }

        public async Task PublishAfterLedger(PayrollLedgerModel payrollLedgerModel)
        {
            await _publishEndpoint.Publish(payrollLedgerModel);
        }
    }
}
