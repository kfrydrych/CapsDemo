using System;
using System.ComponentModel.DataAnnotations;
using UserAccessReview.SharedKernel.Domain;

namespace CapsDemo.ServiceOne.Employees
{
    public class Employee : Entity, IAggregateRoot
    {
        public Guid Id { get; protected set; }

        [Required]
        public string Name { get; protected set; }

        private Employee()
        {
        }

        public Employee(string name)
        {
            Id = Guid.NewGuid();
            Name = name;

            AddDomainEvent(new EmployeeCreatedEvent(Id, Name));
        }
    }
}