using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMQConnection
    {
        public static IModel ConnectToRabbitMQ(string queueName, string routingKey)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMqConsts.Host)
            };
            IConnection connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(queueName, ExchangeType.Fanout);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, queueName, routingKey);
            return channel;
        }
    }
}
