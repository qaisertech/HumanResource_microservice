using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMqConsts
    {
        public const string Host = "amqp://guest:guest@localhost:5672";
        public const string RabbitMqRootUri = "rabbitmq://localhost";
        public const string RabbitMqUri = "rabbitmq://localhost/todoQueue";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string LeaveQueue = "Leave_Queue";
        public const string PayrollQueue = "Payroll_Queue";
    }
}
