using Employee.Microservice.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Microservice.IRepository
{
    public interface IEmployeeRepository
    {
        EmployeeModel GetEmployeeById(Guid Id);
        IEnumerable<EmployeeModel> GetEmployees();
        IEnumerable<EmployeeModel> GetAdmins();
        Task AddEmployee(EmployeeModel model);
        Task UpdateEmployee(EmployeeModel model);
    }
}
