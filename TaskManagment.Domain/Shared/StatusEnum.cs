using TaskManagment.Domain.Exceptions;

namespace TaskManagment.Domain.Shared
{
    public class StatusEnum : Enumeration
    {
        public static StatusEnum DESACTIVADO = new StatusEnum(0, "DESACTIVADO");
        public static StatusEnum ACTIVO = new StatusEnum(1, "ACTIVO");
        public static StatusEnum PENDIENTE = new StatusEnum(2, "PENDIENTE");

        public StatusEnum(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<StatusEnum> List() =>
            new[] { ACTIVO, DESACTIVADO };

        public static StatusEnum FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new TaskManagmentApiDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static StatusEnum From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new TaskManagmentApiDomainException($"Possible values for TypePayment: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

    }

}
