using ServiceBus.Distributed.Commands;

namespace CapsDemo.ServiceOne.Messaging
{
    public class OrderUniformForNewEmployee : ICommand
    {
        public string Name { get; set; }
        public string Size { get; set; }
    }
}