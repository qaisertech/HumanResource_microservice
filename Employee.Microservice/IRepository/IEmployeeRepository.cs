using Employee.Microservice.Models;
using System.Collections;
using System.Collections.Generic;

namespace Employee.Microservice.IRepository
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetEmployees();
    }
}
