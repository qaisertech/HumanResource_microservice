using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leave.Microservice.Model
{
    [Table("Leave")]
    public class LeaveModel
    {
        [Key]
        public Guid LeaveID { get; set; }
        public Guid EmployeeID { get; set; }
        public string LeaveName { get; set; }
        public int NumberOfDays { get; set; }
        public string Status { get; set; }
    }
}
