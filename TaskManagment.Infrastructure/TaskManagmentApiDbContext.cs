using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;
using TaskManagment.Domain.Shared;
using TaskManagment.Shared;

namespace TaskManagment.Infraestructura
{
    public class TaskManagmentApiDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "vtf";
        //Para FK revisar clase ArchivosEntityTypeConfiguration línea 43
        // SolicitudEntityTypeConfiguration línea 140 al 143

        // Caso1: Ejemplo si tenemos Order y OrderDetail
        // Iría sólo Order, debido a que OrdeDetail tiene una FK directo

        // Caso2: Almacen no está vinculado directamente con el retiro
       //public DbSet<Category> Categories { get; set; }

        //public DbSet<MovimientoStock> MovimientosStock { get; set; }
        //public DbSet<Producto> Productos { get; set; }


        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public TaskManagmentApiDbContext(DbContextOptions<TaskManagmentApiDbContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public TaskManagmentApiDbContext(DbContextOptions<TaskManagmentApiDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));           
            System.Diagnostics.Debug.WriteLine("AlmacenApiContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            ;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //SetAudit();
            //var auditEntries = OnBeforeSaveChanges(_identidadServicio.GetUserId());

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);
            // Si falla la integración hace rollback
            //await _mediator.DispatchIntegrationEventsAsync(this);
            //object r = null;
            //foreach (var auditEntry in auditEntries)
            //{
            //    ChangeTracker.DetectChanges();
            //    foreach (var entry in ChangeTracker.Entries())
            //    {
            //        foreach (var property in entry.Properties.Where(x => x.Metadata.IsPrimaryKey()))
            //        {
            //            string propertyName = property.Metadata.GetColumnName();
            //            if (property.Metadata.IsPrimaryKey())
            //            {
            //                auditEntry.KeyValues[propertyName] = property.CurrentValue;
            //                r = property.CurrentValue;
            //            }
            //        }
            //    }
            //    AuditLogs.Add(auditEntry.ToAudit());
            //}
            return true;
        }

        public async Task<bool> SaveEntitiesBulkAsync(Entity entityBulk, CancellationToken cancellationToken = default(CancellationToken))
        {
            //SetAudit();
            //var auditEntries = OnBeforeSaveChanges(_identidadServicio.GetUserId());

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);
            await _mediator.DispatchBulksAsync(this, entityBulk);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);
            // Si falla la integración hace rollback
            //await _mediator.DispatchIntegrationEventsAsync(this);
            //object r = null;
            //foreach (var auditEntry in auditEntries)
            //{
            //    ChangeTracker.DetectChanges();
            //    foreach (var entry in ChangeTracker.Entries())
            //    {
            //        foreach (var property in entry.Properties.Where(x => x.Metadata.IsPrimaryKey()))
            //        {
            //            string propertyName = property.Metadata.GetColumnName();
            //            if (property.Metadata.IsPrimaryKey())
            //            {
            //                auditEntry.KeyValues[propertyName] = property.CurrentValue;
            //                r = property.CurrentValue;
            //            }
            //        }
            //    }
            //    AuditLogs.Add(auditEntry.ToAudit());
            //}
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
                // Se puede mejorar aplicando patrón outbox o failover
                // Si falla la integración no hace rollback
                //await _mediator.DispatchIntegrationEventsAsync(this);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
