using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Microservice.Models
{
    [Table("Employee")]
    public class EmployeeModel
    {
        [Key]
        public Guid EmployeeID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string ReportingTo { get; set; }
    }
}
