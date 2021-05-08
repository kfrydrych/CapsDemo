using CapsDemo.ServiceOne.Shared;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CapsDemo.ServiceOne.Users
{
    public class CreateUser : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User(request.Id, request.Name);

            await _unitOfWork.Repositories.Users.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}