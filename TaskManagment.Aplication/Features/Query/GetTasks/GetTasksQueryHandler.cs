

using MediatR;
using TaskManagment.Domain.Context.AggregatesModel.Tasks;

namespace TaskManagment.Aplication.Features.Query.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskEF>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskEF>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var response = _taskRepository.GEtTaks();
            return response;
        }
    }
}
