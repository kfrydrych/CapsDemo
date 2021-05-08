using System;
using Newtonsoft.Json;
using UserAccessReview.SharedKernel.Domain;

namespace CapsDemo.ServiceOne.Users
{
    public class UserCreatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Username { get; }

        [JsonConstructor]
        public UserCreatedEvent(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}