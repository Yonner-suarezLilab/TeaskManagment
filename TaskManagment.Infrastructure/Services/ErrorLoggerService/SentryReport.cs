using Sentry;
using Serilog;

namespace TaskManagment.Infraestructura.Services.ErrorLoggerService
{
    public class SentryReport : IErrorLoggerService
    {
        public void LogError(Exception ex)
        {
            try
            {
                ex.Data.Add("Stack trace", ex.StackTrace);
                SentrySdk.CaptureException(ex);
            }
            catch (Exception exs)
            {
                Log.Error(exs.Message);
            }
        }
    }
}