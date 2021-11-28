using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Models
{
    public class LeaveConsumerModel
    {
        public Guid LeaveID { get; set; }
        public Guid EmployeeID { get; set; }
        public string LeaveName { get; set; }
        public int NumberOfDays { get; set; }
        public string Status { get; set; }
    }
}
