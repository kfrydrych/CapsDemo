using System.Threading.Tasks;
using CapsDemo.ServiceOne.Shared;
using CapsDemo.ServiceOne.Users;
using DotNetCore.CAP;
using ServiceBus.Distributed.Commands;

namespace CapsDemo.ServiceOne.Messaging
{
    public class OnUserCreated_SentToAzureServiceBus : IListener<UserCreatedEvent>
    {
        private readonly ICommandBus _commandBus;

        public OnUserCreated_SentToAzureServiceBus(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [CapSubscribe(nameof(UserCreatedEvent), Group = "ServiceBus")]
        public async Task Handle(UserCreatedEvent e)
        {
            var cmd = new UpdateActiveDirectory
            {
                UserId = e.UserId,
                Username = e.Username,
                Scope = "calendar, agent, team"
            };

            await _commandBus.SendAsync(cmd);
        }
    }
}