using Employee.Microservice.IRepository;
using Employee.Microservice.Models;
using Employee.Microservice.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<ActionResult> Add([FromBody] EmployeeViewModel viewModel)
        {
            var model = new EmployeeModel()
            {
                Name = viewModel.Name,
                Address = viewModel.Address,
                Age = viewModel.Age,
                DOB = viewModel.DOB,
                ReportingTo = viewModel.ReportingTo,
                Role = viewModel.Role
            };
            await _employeeRepository.AddEmployee(model);
            return Ok();
        }

        [HttpGet]
        [Route("getAdmins")]
        public ActionResult GetAdmins()
        {
            return Ok(_employeeRepository.GetAdmins());
        }
    }
}
