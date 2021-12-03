using System;

namespace Employee.Microservice.ViewModel
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Guid? ReportingTo { get; set; }= Guid.Empty;
    }
}
