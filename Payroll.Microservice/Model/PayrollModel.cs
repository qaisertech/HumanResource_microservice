using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Microservice.Model
{
    [Table("Payroll")]
    public class PayrollModel
    {
        [Key]
        public Guid PayrollID { get; set; }
        public Guid EmployeeID { get; set; }
        public int NoOfLeaves { get; set; }
        public int Deductions { get; set; }
    }
}
