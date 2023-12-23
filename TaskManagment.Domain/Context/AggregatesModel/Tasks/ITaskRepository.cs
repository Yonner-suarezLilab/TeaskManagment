

using TaskManagment.Domain.Shared;

namespace TaskManagment.Domain.Context.AggregatesModel.Tasks
{
    public interface ITaskRepository: IRepository<TaskEF>
    {
        void Add(TaskEF task);
        List<TaskEF> GEtTaks();
    }
}
