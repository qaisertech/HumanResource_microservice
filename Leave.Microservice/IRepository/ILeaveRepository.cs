using Leave.Microservice.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leave.Microservice.IRepository
{
    public interface ILeaveRepository
    {
        IEnumerable<LeaveModel> GetLeaves { get; set; }
        Task AddLeave(LeaveModel leave);
    }
}
