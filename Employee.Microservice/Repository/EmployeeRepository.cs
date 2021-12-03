using Employee.Microservice.Data;
using Employee.Microservice.IRepository;
using Employee.Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Microservice.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public EmployeeModel GetEmployeeById(Guid Id)
        {
            return _employeeContext.Employees.FirstOrDefault(o=> o.EmployeeID == Id);
        }
        public async Task AddEmployee(EmployeeModel model)
        {
            _employeeContext.Add<EmployeeModel>(model);
            await _employeeContext.SaveChangesAsync();
        }

        public IEnumerable<EmployeeModel> GetAdmins()
        {
            var result = _employeeContext.Employees.Where(o => o.ReportingTo != null || o.ReportingTo != System.Guid.Empty).ToList();
            return result;
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            var result = _employeeContext.Employees.ToList();
            return result;
        }

        public async Task UpdateEmployee(EmployeeModel model)
        {
            var employee = _employeeContext.Employees.FirstOrDefault(o=> o.EmployeeID == model.EmployeeID);
            if (employee != null)
            {
                employee = model;
            }
            await _employeeContext.SaveChangesAsync();
        }
    }
}
