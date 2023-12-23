
using TaskManagment.Domain.Context.AggregatesModel.Tasks;
using TaskManagment.Domain.Shared;

namespace TaskManagment.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly FakeDataBase _fakeDatabase;

        public TaskRepository(FakeDataBase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase ?? throw new ArgumentNullException(nameof(fakeDatabase));
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(TaskEF task)
        {
            _fakeDatabase.Tasks.Add(task);
        }

        public List<TaskEF> GEtTaks()
        {
            return _fakeDatabase.Tasks.ToList();
        }
    }
}
