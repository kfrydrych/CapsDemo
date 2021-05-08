using System;
using ServiceBus.Distributed.Commands;

namespace CapsDemo.ServiceOne.Messaging
{
    public class UpdateActiveDirectory : ICommand
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Scope { get; set; }
    }
}