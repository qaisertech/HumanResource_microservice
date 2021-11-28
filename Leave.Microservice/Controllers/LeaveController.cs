using Leave.Microservice.IRepository;
using Leave.Microservice.Model;
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
        public IActionResult AddLeave([FromBody] LeaveModel leaveModel)
        {
            _leaveRepository.AddLeave(leaveModel);
            return Ok();
        }
    }
}
