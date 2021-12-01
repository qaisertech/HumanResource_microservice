using Payroll.Microservice.Model;
using RabbitMQ.Models;
using System.Threading.Tasks;

namespace Payroll.Microservice.IRepository
{
    public interface IPayrollRepository
    {
        void AddPayroll(PayrollModel model);
        Task PublishAfterLedger(PayrollLedgerModel payrollLedgerModel);
    }
}
