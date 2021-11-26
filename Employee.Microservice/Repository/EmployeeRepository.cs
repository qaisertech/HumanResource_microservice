using Employee.Microservice.Data;
using Employee.Microservice.IRepository;
using Employee.Microservice.Models;
using System.Collections.Generic;
using System.Linq;

namespace Employee.Microservice.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            var result = _employeeContext.Employees.ToList();
            return result;
        }
    }
}
