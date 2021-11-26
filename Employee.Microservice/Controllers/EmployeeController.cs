using Employee.Microservice.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Microservice.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_employeeRepository.GetEmployees());
        }
    }
}
