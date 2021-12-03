using Leave.Microservice.Model;
using RabbitMQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leave.Microservice.IRepository
{
    public interface ILeaveRepository
    {
        Task<IEnumerable<LeaveModel>> GetAllLeaves();
        void AddLeave(LeaveModel leave);
        void UpdateLeave(PayrollLedgerModel model);
    }
}
