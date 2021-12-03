using Leave.Microservice.Data;
using Leave.Microservice.IRepository;
using Leave.Microservice.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using RabbitMQ;
using MassTransit;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using RabbitMQ.Models;
using System.Linq;
using Refit;

namespace Leave.Microservice.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly LeaveContext _leaveContext;
        private readonly LeaveConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveRepository(LeaveContext leaveContext, IOptions<LeaveConfiguration> configuration, IPublishEndpoint publishEndpoint, IEmployeeRepository employeeRepository)
        {
            _publishEndpoint = publishEndpoint;
            _leaveContext = leaveContext;
            _configuration = configuration.Value;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<LeaveModel>> GetAllLeaves()
        {

            var leaves = _leaveContext.Leaves.ToList();
            //foreach (var leave in leaves)
            //{
            //    try
            //    {
            //        var res = await _employeeRepository.GetEmployeeById(leave.EmployeeID);
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //}
            return leaves;
        }
        public void AddLeave(LeaveModel leave)
        {
            try
            {
                _leaveContext.Add<LeaveModel>(leave);
                _leaveContext.SaveChanges();
                PublishToPayroll(leave);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task PublishToPayroll(LeaveModel model)
        {
            var leaveConsumerModel = new LeaveConsumerModel()
            {
                EmployeeID = model.EmployeeID,
                LeaveName = model.LeaveName,
                NumberOfDays = model.NumberOfDays,
                LeaveID = model.LeaveID
            };
            //var channel = RabbitMQConnection.ConnectToRabbitMQ(_configuration.LeaveQueue, "leave.init");

            //var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(leaveModel));
            //channel.BasicPublish(_configuration.LeaveQueue, "leave.init", true, null, body);


            //var uri = new Uri(RabbitMqConsts.RabbitMqRootUri+"/"+_configuration.LeaveQueue);
            //var endpoint = await _bus.GetSendEndpoint(uri);
            await _publishEndpoint.Publish(leaveConsumerModel);
            //await endpoint.Send(leaveModel);
        }

        public void UpdateLeave(PayrollLedgerModel model)
        {
            var leave = _leaveContext.Leaves.FirstOrDefault(o => o.LeaveID == model.LeaveID);
            if (leave != null)
            {
                leave.Status = model.Status;
                _leaveContext.Update(leave);
                _leaveContext.SaveChanges();
            }
        }

    //    using var client = httpClientFactory.CreateClient("order");
    //            var response = await client.GetAsync("/api/order");
    //var data = await response.Content.ReadAsStringAsync();
    //            return JsonConvert.DeserializeObject<OrderDetail[]>(data);
    }
}
