using System;
using Newtonsoft.Json;
using UserAccessReview.SharedKernel.Domain;

namespace CapsDemo.ServiceOne.Employees
{
    public class EmployeeCreatedEvent : DomainEvent
    {
        public Guid EmployeeId { get; }
        public string EmployeeName { get; }

        [JsonConstructor]
        public EmployeeCreatedEvent(Guid employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }
    }
}