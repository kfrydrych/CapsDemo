using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserAccessReview.SharedKernel.Domain;

namespace CapsDemo.ServiceOne.Shared
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICapPublisher _publisher;
        private readonly AppDbContext _dbContext;
        private readonly IDbContextTransaction _transaction;

        public UnitOfWork(ICapPublisher publisher, AppDbContext dbContext)
        {
            _publisher = publisher;
            _dbContext = dbContext;
            _transaction = dbContext.Database.BeginTransaction(_publisher);
        }

        public IRepositories Repositories => _dbContext;
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var domainEvents = GetAllDomainEvents();

            ClearAllDomainEvents();

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                var type = domainEvent.GetType();
                await _publisher.PublishAsync(type.Name, domainEvent, cancellationToken: cancellationToken);
            }
            await _transaction.CommitAsync(cancellationToken);

            return result;
        }

        private IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }
        private void ClearAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }

    public interface IUnitOfWork
    {
        IRepositories Repositories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}