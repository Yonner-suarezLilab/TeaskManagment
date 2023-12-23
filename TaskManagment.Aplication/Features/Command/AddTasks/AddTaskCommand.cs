using MediatR;

namespace TaskManagment.Aplication.Features.Command.AddTasks
{
    public class AddTaskCommand : IRequest<Unit>
    {
        public string User { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public AddTaskCommand()
        {
        }

        public AddTaskCommand(string user, string description, DateTime creationDate)
        {
            User = user;
            Description = description;
            CreationDate = creationDate;
        }
    }
}
