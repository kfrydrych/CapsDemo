using System;
using UserAccessReview.SharedKernel.Domain;

namespace CapsDemo.ServiceOne.Users
{
    public class User : Entity, IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        private User()
        {
        }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;

            AddDomainEvent(new UserCreatedEvent(Id, Name));
        }
    }
}