using Leave.Microservice.Model;
using RabbitMQ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leave.Microservice.IRepository
{
    public interface ILeaveRepository
    {
        IEnumerable<LeaveModel> GetLeaves { get; set; }
        void AddLeave(LeaveModel leave);
        void UpdateLeave(PayrollLedgerModel model);
    }
}
