

using System.Globalization;
using TaskManagment.Domain.Shared;

namespace TaskManagment.Domain.Context.AggregatesModel.Tasks
{
    public class TaskEF : Entity, IAggregateRoot
    {
        public string User { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }

    public class FakeDataBase
    {
        public List<TaskEF> Tasks { get; set; } = new List<TaskEF>();
    }
}
