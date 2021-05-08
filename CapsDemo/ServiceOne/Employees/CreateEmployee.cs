using CapsDemo.ServiceOne.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CapsDemo.ServiceOne.Employees
{
    public class CreateEmployee : IRequest
    {
        public string Name { get; set; }
    }

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var emp = new Employee(request.Name);

            await _unitOfWork.Repositories.Employees.AddAsync(emp, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}