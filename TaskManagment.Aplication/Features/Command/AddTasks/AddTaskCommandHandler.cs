

using MediatR;
using TaskManagment.Domain.Context.AggregatesModel.Tasks;

namespace TaskManagment.Aplication.Features.Command.AddTasks
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Unit>
    {
        private readonly ITaskRepository _taskRepository;

        public AddTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new TaskEF()
            {
                CreationDate = request.CreationDate,
                Description = request.Description,
                User = request.User
            };

            _taskRepository.Add(newTask);

            return Unit.Value;
        }
    }
}
