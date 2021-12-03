using Leave.Microservice.Model;
using Refit;
using System;
using System.Threading.Tasks;

namespace Leave.Microservice.IRepository
{
    public interface IEmployeeRepository
    {
        [Get("/api/Employee/getemployeebyid/{id}")]
        Task<EmployeeModel> GetEmployeeById(Guid id);
    }
}
