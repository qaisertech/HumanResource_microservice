using Leave.Microservice.IRepository;
using Leave.Microservice.Model;
using Leave.Microservice.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Leave.Microservice.Controllers
{
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveController(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        [HttpPost]
        [Route("AddLeave")]
        public IActionResult AddLeave([FromBody] LeaveViewModel leaveModel)
        {
            var model = new LeaveModel()
            {
                EmployeeID = leaveModel.EmployeeID,
                LeaveName = leaveModel.LeaveName,
                NumberOfDays = leaveModel.NumberOfDays
            };
            _leaveRepository.AddLeave(model);
            return Ok();
        }
    }
}
