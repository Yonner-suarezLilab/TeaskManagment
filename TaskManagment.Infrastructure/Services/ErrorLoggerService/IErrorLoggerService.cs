namespace TaskManagment.Infraestructura.Services.ErrorLoggerService
{
    public interface IErrorLoggerService
    {
        void LogError(Exception ex);
    }
}