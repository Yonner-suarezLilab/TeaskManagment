using Microsoft.Extensions.Options;
using TaskManagment.Shared.Configuration;

namespace TaskManagment.Api.Infraestructura
{
    public class AppConfig : IAppConfig
    {
        public AppConfiguration Configuration { get; set; }
        public AppConfig(IOptions<AppConfiguration> config)
        {
            Configuration = config.Value;
        }
    }
}
