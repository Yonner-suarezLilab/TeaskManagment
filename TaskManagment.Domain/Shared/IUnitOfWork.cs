namespace TaskManagment.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Esta entidad es la que debe contener los eventos de Domains,
        /// Eventos de Domains fuera de esta entidad principal, no serán ejecutados
        /// </summary>
        /// <param name="entityBulk"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesBulkAsync(Entity entityBulk, CancellationToken cancellationToken = default);
    }
}
