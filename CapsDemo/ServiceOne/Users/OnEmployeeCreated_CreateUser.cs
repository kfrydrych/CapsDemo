using System.Threading.Tasks;
using CapsDemo.ServiceOne.Employees;
using CapsDemo.ServiceOne.Shared;
using DotNetCore.CAP;
using MediatR;

namespace CapsDemo.ServiceOne.Users
{
    public class OnEmployeeCreated_CreateUser : IListener<EmployeeCreatedEvent>
    {
        private readonly IMediator _mediator;

        public OnEmployeeCreated_CreateUser(IMediator mediator)
        {
            _mediator = mediator;
        }

        [CapSubscribe(nameof(EmployeeCreatedEvent), Group = "Users")]
        public async Task Handle(EmployeeCreatedEvent e)
        {
            var command = new CreateUser
            {
                Id = e.EmployeeId,
                Name = e.EmployeeName
            };

            await _mediator.Send(command);
        }
    }
}