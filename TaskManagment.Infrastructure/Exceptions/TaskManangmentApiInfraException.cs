using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagment.Infraestructura.Exceptions
{
    public class TaskManangmentApiInfraException : Exception
    {
        public TaskManangmentApiInfraException(string message)
          : base(message)
        { }
        public TaskManangmentApiInfraException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
