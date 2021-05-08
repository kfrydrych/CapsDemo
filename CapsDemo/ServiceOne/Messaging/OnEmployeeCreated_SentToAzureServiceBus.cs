using System.Threading.Tasks;
using CapsDemo.ServiceOne.Employees;
using CapsDemo.ServiceOne.Shared;
using DotNetCore.CAP;
using ServiceBus.Distributed.Commands;

namespace CapsDemo.ServiceOne.Messaging
{
    public class OnEmployeeCreated_SentToAzureServiceBus : IListener<EmployeeCreatedEvent>
    {
        private readonly ICommandBus _commandBus;

        public OnEmployeeCreated_SentToAzureServiceBus(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [CapSubscribe(nameof(EmployeeCreatedEvent), Group = "ServiceBus")]
        public async Task Handle(EmployeeCreatedEvent e)
        {
            var cmd = new OrderUniformForNewEmployee
            {
                Name = e.EmployeeName,
                Size = "XL"
            };

            await _commandBus.SendAsync(cmd);
        }
    }
}