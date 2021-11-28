using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class Publisher
    {
        public static void PublishMessage(IModel channel, object message)
        {
            //var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            //channel.BasicPublish(_configuration.LeaveQueue, "leave.init", true, null, body);
        }
    }
}
