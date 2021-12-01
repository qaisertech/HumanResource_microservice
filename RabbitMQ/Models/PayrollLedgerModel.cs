using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Models
{
    public class PayrollLedgerModel
    {
        public Guid EmployeeID { get; set; }
        public Guid LeaveID { get; set; }
        public string Status { get; set; }
    }
}
