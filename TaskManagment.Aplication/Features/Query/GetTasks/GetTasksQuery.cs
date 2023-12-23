
using MediatR;
using TaskManagment.Domain.Context.AggregatesModel.Tasks;

namespace TaskManagment.Aplication.Features.Query.GetTasks
{
    public class GetTasksQuery : IRequest<List<TaskEF>>
    {
        public GetTasksQuery()
        {
        }
    }
}
