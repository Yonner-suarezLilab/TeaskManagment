namespace TaskManagment.Domain.Exceptions
{
    public class TaskManagmentApiDomainException : Exception
    {
        public TaskManagmentApiDomainException(string message)
          : base(message)
        { }
        public TaskManagmentApiDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
