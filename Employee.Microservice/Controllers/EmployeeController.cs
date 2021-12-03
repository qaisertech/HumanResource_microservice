using Employee.Microservice.IRepository;
using Employee.Microservice.Models;
using Employee.Microservice.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_employeeRepository.GetEmployees());
        }

        [Authorize]
        [HttpGet]
        [Route("getemployeebyid/{id}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            return Ok(_employeeRepository.GetEmployeeById(id));
        }

        [HttpPost]
        [Authorize]
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

        [HttpGet("Privacy")]
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }
    }
}
