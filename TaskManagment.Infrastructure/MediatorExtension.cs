using MediatR;
using TaskManagment.Domain.Shared;

namespace TaskManagment.Infraestructura
{
    static class MediatorExtension 
    {
         
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, TaskManagmentApiDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());


            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }

        public static async Task DispatchBulksAsync(this IMediator mediator, TaskManagmentApiDbContext ctx, Entity builkEntity)
        {
            //TODO: Obtener cambios sin el changetracker
            //var domainEntities = ctx.ChangeTracker
            //    .Entries<Entity>()
            //    .Where(x => 
            //    x.Entity.DomainEvents != null &&
            //    x.Entity.IdGlobal == builkEntity.IdGlobal
            //    && x.Entity.DomainEvents.Any());

            var domainEvents = builkEntity.DomainEvents
                .ToList();

            builkEntity.ClearDomainEvents();

            //builkEntity.DomainEvents.ToList()
            //    .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }

    }
}
