namespace TaskManagment.Api.Infraestructura
{
    public class VersionUtil
    {
        public string GetAssemblyVersion()
        {
            var version = GetType().Assembly.GetName().Version;
            if (version != null)
            {
                return version.ToString();
            }
            return "";
        }
    }
}
