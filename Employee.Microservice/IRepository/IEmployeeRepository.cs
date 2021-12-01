using Employee.Microservice.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Microservice.IRepository
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetEmployees();
        IEnumerable<EmployeeModel> GetAdmins();
        Task AddEmployee(EmployeeModel model);
        Task UpdateEmployee(EmployeeModel model);
    }
}
